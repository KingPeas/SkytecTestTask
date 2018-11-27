using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class EnemyBrain : UnitBrain
    {
        EnemyData myData = null;
        CharacterData target = null;
        private void Start()
        {
            myData = Data as EnemyData;
            if (!myData) enabled = false;
        }
        public override void InitFSM()
        {
            base.InitFSM();
            fsm.AddStates(new[] { IDLE, MOVE, RUN, JUMP, BLOCK, ATTACK, FOLLOW, DIE });
            fsm.AnyState().To(DIE).If(m => Data.IsDestroyed);
            fsm.AnyState().To(ATTACK).If(m => myData.ai.detectAttackZone && Data.attack.CanAttack).Jump(Attack);
            fsm.AnyState().To(JUMP).If(m => !Data.move.IsGrounded && m.current.name != ATTACK);
            fsm.In().To(IDLE);
            fsm.In(IDLE).To(MOVE).If(m => m.Timer > 1f); // минуту приходим в себя после появления
            fsm.In(MOVE).To(RUN).If(m => myData.ai.player != null).Jump(() => target = myData.ai.player);
            fsm.In(ATTACK).To(FOLLOW).If(m => m.Timer >= Data.attack.TimeAttack);
            fsm.In(JUMP).To(FOLLOW).If(m => Data.move.IsGrounded && m.Timer > 0.1);
            fsm.In(RUN).To(BLOCK).If(m => myData.ai.detectBullet);
            fsm.In(RUN).To(FOLLOW).If(m => myData.ai.player == null);
            fsm.In(RUN).To(JUMP).If(m => CanJump()).Jump(Jump);
            fsm.In(BLOCK).To(FOLLOW).If(m => m.Timer > myData.ai.TimeBlock);
            fsm.In(FOLLOW).To(RUN).If(m => myData.ai.player != null);
            fsm.In(FOLLOW).To(MOVE).If(m => m.Timer > myData.ai.TimeFollowing).Jump(() => target = myData.ai.player);
            fsm.In(FOLLOW).To(JUMP).If(m => CanJump()).Jump(Jump);


            //fsm
            fsm.In(IDLE).Enter(() => SetState(BrainState.Idle));
            fsm.In(DIE).Enter(() => { fsm.AnyState().noMoreTransitions = true; SetState(BrainState.Die);  Destroy(gameObject, 3f); });
            fsm.In(JUMP).Enter(() => SetState(BrainState.Jump)).Stay(Run);
            fsm.In(MOVE).Enter(() => SetState(BrainState.Move)).Stay(Move);
            fsm.In(RUN).Enter(() => SetState(BrainState.Run)).Stay(Run);
            fsm.In(FOLLOW).Enter(() => SetState(BrainState.Run)).Stay(Run);
            fsm.In(BLOCK).Enter(() => SetState(BrainState.Block)).Exit(() => myData.ai.detectBullet = false);
            fsm.In(ATTACK).Enter(() => { Data.attack.Active = false; Data.attack.CanAttack = false; SetState(BrainState.Attack); ActivateWepon(true); })
                .Exit(() => { Data.attack.CanAttack = true; ActivateWepon(false); });
            fsm.Start();
        }

        private bool CanJump()
        {
            if (!target) return false;
            return Data.move.IsGrounded && !Data.move.UnderCover &&  MoveRight() && (myData.ai.detectHole || myData.ai.detectBarrier) && target.transform.position.y > (transform.position.y + 0.2f);
        }

        private void Jump()
        {
            if (Data.avatar.body)
            {
                Data.avatar.body.AddForce(Vector2.up * Data.move.ForceJump, ForceMode2D.Impulse);
                Debug.Log("Я кузнечик");
            }
        }

        private void Move()
        {
            Vector2 dir = ChangeDirection(myData.ai.detectHole || myData.ai.detectBarrier);

            if (Mathf.Abs(dir.x) > float.Epsilon)
            {

                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(dir.x, 0, 0), Data.move.speed * Time.deltaTime);
            }
        }

        private void Run()
        {
            
            Vector2 dir = ChangeDirection(Data.move.IsGrounded && !MoveRight());
            if (Data.avatar.body)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(dir.x, 0, 0), myData.ai.SpeedRun * Time.deltaTime);

            }

        }

        private bool MoveRight()
        {
            if (!target) return true;
            Vector2 targetDir = target.transform.position - transform.position;
            Vector2 dir = Data.move.moveDirection;
            return Vector2.Dot(targetDir, dir) > 0;
        }

        private Vector2 ChangeDirection(bool needChange)
        {
            if (needChange)
            {
                Data.move.moveDirection = -Data.move.moveDirection;
                Data.avatar.flip = !Data.avatar.flip;
            }
            return Data.move.moveDirection;
        }
        private void Attack()
        {
            if (Data.attack.CanAttack)
            {
                Data.attack.Active = true;
                Data.attack.StateAttack = BrainState.Attack;
                Data.attack.numWeapon = 0;
            }
        }
    }

    
}