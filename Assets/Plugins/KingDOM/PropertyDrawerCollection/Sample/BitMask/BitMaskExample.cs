using System;
using UnityEngine;

[ExecuteInEditMode]  //to reflect the update properties in the editor is "setHigh" and "Test"
public class BitMaskExample : MonoBehaviour
{
    [Flags]
    public enum FilmQuality
    {
        //[EnumLabel(" ")]
        //None = 0,
        [EnumLabel("Perfect")]
        High = 1,
        [EnumLabel]
        [PropertyArgs(label = "Color")]
        ColorInfo = 2,
        [EnumLabel]
        [PropertyArgs(label = "Subtitles")]
        SubtitlesExist = 4,
        [EnumLabel("All")]
        Perfect = 7
    }

    [Flags]
    public enum FilmPrice
    {
        //[EnumLabel(" ")]
        //None = 0,
        High = 2,
        [EnumLabel]
        [PropertyArgs(label = "Excellent")]
        Super = 128,
        [EnumLabel]
        [PropertyArgs(label = "Really bad")]
        Foo = 1
    }

    [BitMask]
    [PropertyArgs(label = "Film list", tip = "Select films options.")]

    public FilmQuality[] testArray = new FilmQuality[3];

    [BitMask]
    [PropertyArgs(label = "Filmez", tip = "Select the option for film.")]
    public FilmQuality testFilm = 0;

    [BitMask]
    [PropertyArgs(tip = "To change this property, you must change the value of property of SetHigh.")]
    public FilmQuality TestMultySelect = FilmQuality.High | FilmQuality.SubtitlesExist;

    [BitMask(typeof(FilmQuality))]
    public int testInt = 7;

    [PropertyBase]
    [PropertyArgs(tip = "Modifying this property, you’re changing the properties of Test.", callbacks = new[] { "Updated" })]
    [Help("You change SetHigh, script will update Test value.")]
    public bool setHigh = false;

    [BitMask(typeof(FilmPrice))]
    [PropertyArgs(tip = "Do you really want to see this film?", callbacks = new[] { "PrintFilmPrice" })]
    public int testNonRegularFlag = 3;

    void Updated()
    {
        TestMultySelect = setHigh ? FilmQuality.High : 0;
    }

    void PrintFilmPrice()
    {
        Debug.Log(string.Format("testNonRegularFlag = {0}", testNonRegularFlag));
    }

}
