using System;
using UnityEngine;

namespace Game.Rhythm
{
    public class RhythmNote : MonoBehaviour
    {
        [SerializeField] float speed = 2;

        public RhythmLine Line { get; set; }

        void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            float distance = Line.GetDistanceFromTarget(this);
            
            if (distance > 6)
            {
                Line.RemovePoint(this);
                Destroy(gameObject);
            }
            else if (distance > 1)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.5f, 0.5f, 0.5f), Time.deltaTime);
            }
        }
    }
}