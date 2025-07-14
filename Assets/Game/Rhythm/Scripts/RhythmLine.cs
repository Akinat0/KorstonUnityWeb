using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Rhythm
{
    public class RhythmLine : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;
        [SerializeField] Transform targetPoint;
        [SerializeField] RhythmNote notePrefab;
        [SerializeField] ClickZone clickZone;
        [SerializeField] TextMeshProUGUI score;

        List<RhythmNote> notes = new List<RhythmNote>(16);
        
        void OnEnable()
        {
            StartCoroutine(SpawnRoutine());
            
            clickZone.OnMouseDownEvent.AddListener(HandleMouseDownEvent);
        }

        IEnumerator SpawnRoutine()
        {
            while (true)
            {
                RhythmNote note = Instantiate(notePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation,transform);
                note.Line = this;
                notes.Add(note);
                
                yield return new WaitForSeconds(1);
            }
        }


        public float GetDistanceFromTarget(RhythmNote note)
        {
            Vector3 notePosInLocalSpace = transform.InverseTransformPoint(note.transform.position);
            Vector3 targetPosInLocalSpace = transform.InverseTransformPoint(targetPoint.position);
            return targetPosInLocalSpace.y - notePosInLocalSpace.y;
        }
        
        void HandleMouseDownEvent()
        {
            for (int i = 0; i < notes.Count; i++)
            {
                
                RhythmNote note = notes[i];
                float dist = GetDistanceFromTarget(note);
                
                if(dist > 1)
                    continue;

                if (dist > -1 && dist < 1)
                {
                    print($"register note dist {dist} ");
                    RemovePoint(note);
                    score.text = ((int.TryParse(score.text, out int val) ? val : 0) + 1).ToString();
                    break;
                }
                
            }
        }

        public void RemovePoint(RhythmNote rhythmNote)
        {
            notes.Remove(rhythmNote);
            Destroy(rhythmNote.gameObject);
        }
    }
}