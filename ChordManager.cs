using ChordsLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsLibrary
{
    public class ChordManager
    {
        //This is where I'm going to manage caching various information for later use and managing in IO for this library
        private ChordDAO dao = new ChordDAO();
        private ChordDTO dto = new ChordDTO();
        private Chord foundChord = new Chord();
        private Chord unknownChord = new Chord();
        private List<Chord> chordList = new List<Chord>();
        private List<Chord> chordsReturned = new List<Chord>();
        private string message;

        public List<Chord> FindChord(List<Note> notesEntered)
        {            
            unknownChord.ChordNoteList = notesEntered;


            if (notesEntered.Count == 0)
            {
                message = "Notes are empty";
            }
            else
            {

                unknownChord.RootNote = notesEntered[0];
                unknownChord.NoteDifference = Chord.FindNoteRelationship(notesEntered);

                ChordCacheLoad(unknownChord);

                if (unknownChord.ChordName == null)
                {
                    chordsReturned = dao.FindAChord(unknownChord);
                    if (chordsReturned.Count == 1)
                    {                       
                        message = "We found your chord!";
                    }
                    else if (chordsReturned.Count > 1)
                    {
                        message = "Your chord may be an inversion of one of these chords";
                    }
                    else
                    {
                        message = "Either this is not a formal chord, or we don't have it in our system yet.";
                    }
                    
                }
                else
                {
                    message = "Either this is not a formal chord, or we don't have it in our system yet.";
                }
            }
            return chordsReturned;
        }

        public string InsertNewChord(Chord newChord)
        {
            if (IsNewChord(newChord))
            {
                chordList = unknownChord.InsertChord(newChord);                
                ChordDTO.InsertNewChord(chordList, dao.DBChords); //need to make sure to query chord lenght and clear old data out of the DAO
                return "Chord added";
            }
            else
            {
                return "There was a problam inserting that chord.";
            }
        }        

        private bool IsNewChord(Chord chord)
        {           
            chord.RootNote = chord.ChordNoteList[0];            
            chord.NoteDifference = Chord.FindNoteRelationship(chord.ChordNoteList);
            if ((chordList == null) || (!chordList.Any()))
            {
                chordsReturned = dao.FindAChord(chord);
            }
            

            //I just picked an element that will be there everytime
            foreach (Chord co in chordsReturned)
            {
                if (co.ChordName == null)
                {
                    return true;
                }

            }

            return false;
        }

        private void ChordCacheLoad(Chord unknownChord)
        {
            if (chordsReturned.Count != 0 &&((chordsReturned == null) || (!chordsReturned.Any())))
            {
                ChordCacheClear(unknownChord); //checks whether the chord count matches the file in memory before deciding
            }

            chordList = dao.LoadChordsByCount(unknownChord.ChordNoteList.Count());
        }

        private void ChordCacheClear(Chord chord)
        {
            if (chord.ChordNoteList.Count() == chordList[0].ChordNoteList.Count())
            {
                return;
            }
            else
            {
                foreach (Chord co in chordsReturned)
                {
                    chordList.Remove(co);
                }
                
            }

        }
    }
}
