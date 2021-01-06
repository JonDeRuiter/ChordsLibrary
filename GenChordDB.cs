using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsLibrary
{
    public class GenChordDB
    {

        //https://en.wikipedia.org/wiki/Chord_(music)
        public static void GenerateChordData()
        {
            GenTriads();
            GenSevenths();
        }

        public static void GenSevenths()
        {
            char sharp = '\x266f'; //UFT-16 encoding
            char flat = '\x266D';

            List<string> noteList = new List<string>();
            noteList.Add("C");
            noteList.Add("E");
            noteList.Add("G");
            noteList.Add("B");
            Chord MajorSeventh = new Chord("C Major Seventh", Note.MapNotes(noteList));
            MajorSeventh.InsertChord(MajorSeventh);
            noteList.Clear();


            noteList.Add("C");
            noteList.Add("E" + sharp);
            noteList.Add("G");
            noteList.Add("B");
            Chord MinorMajSeventh = new Chord("C Minor Major Seventh", Note.MapNotes(noteList));
            MinorMajSeventh.InsertChord(MinorMajSeventh);
            noteList.Clear();

            noteList.Add("C");
            noteList.Add("E" + sharp);
            noteList.Add("G");
            noteList.Add("B" + flat);
            Chord MinorSeventh = new Chord("C Minor Seventh", Note.MapNotes(noteList));
            MinorSeventh.InsertChord(MinorSeventh);
            noteList.Clear();

            noteList.Add("C");
            noteList.Add("E" + flat);
            noteList.Add("G" + flat);
            noteList.Add("A"); //b double flat
            Chord DimSeventh = new Chord("C Diminished Seventh", Note.MapNotes(noteList));
            DimSeventh.InsertChord(DimSeventh);
            noteList.Clear();

            noteList.Add("C");
            noteList.Add("E" + flat);
            noteList.Add("G" + flat);
            noteList.Add("B" + flat);
            Chord HalfDimSeventh = new Chord("C Half-Diminished Seventh", Note.MapNotes(noteList));
            HalfDimSeventh.InsertChord(HalfDimSeventh);
            noteList.Clear();

            noteList.Add("C");
            noteList.Add("E");
            noteList.Add("G" + sharp);
            noteList.Add("B" + flat);
            Chord AugmentedSeventh = new Chord("C Augmented Seventh", Note.MapNotes(noteList));
            AugmentedSeventh.InsertChord(AugmentedSeventh);
            noteList.Clear();

            noteList.Add("C");
            noteList.Add("E");
            noteList.Add("G" + sharp);
            noteList.Add("B");
            Chord AugmentedMajSeventh = new Chord("C Augmented Major Seventh", Note.MapNotes(noteList));
            AugmentedMajSeventh.InsertChord(AugmentedMajSeventh);
            noteList.Clear();

            noteList.Add("G");
            noteList.Add("B");
            noteList.Add("D");
            noteList.Add("F");
            Chord DominantSeventh = new Chord("G Dominant Seventh", Note.MapNotes(noteList));
            DominantSeventh.InsertChord(DominantSeventh);
            noteList.Clear();
        }

        public static void GenTriads()
        {
            char sharp = '\x266f'; //UFT-16 encoding
            char flat = '\x266D';

            List<string> noteList = new List<string>();
            noteList.Add("C");
            noteList.Add("E");
            noteList.Add("G");
            Chord MajorTriad = new Chord("C Major Triad", Note.MapNotes(noteList));
            MajorTriad.InsertChord(MajorTriad);
            noteList.Clear();

            noteList.Add("A");
            noteList.Add("C");
            noteList.Add("G");
            Chord MinorTriad = new Chord("A Minor Triad", Note.MapNotes(noteList));
            MinorTriad.InsertChord(MinorTriad);
            noteList.Clear();

            noteList.Add("B");
            noteList.Add("D");
            noteList.Add("F");
            Chord DiminishedTriad = new Chord("B Diminished Triad", Note.MapNotes(noteList));
            DiminishedTriad.InsertChord(DiminishedTriad);
            noteList.Clear();

            noteList.Add("D");
            noteList.Add("F" + sharp);
            noteList.Add("A" + sharp);
            Chord AugmentedTriad = new Chord("D Augmented Triad", Note.MapNotes(noteList));
            AugmentedTriad.InsertChord(AugmentedTriad);
            noteList.Clear();
        }
    }
}
