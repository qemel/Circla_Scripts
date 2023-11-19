using UnityEngine;

namespace App.Scripts.Models.Songs.Notes
{
    public interface IRotInput
    {
        float MinAngle { get; }
        float MaxAngle { get; }
    }

    public record RotInputUp : IRotInput
    {
        public float MinAngle => 45;
        public float MaxAngle => 135;
    }

    public record RotInputDown : IRotInput
    {
        public float MinAngle => 225;
        public float MaxAngle => 315;
    }

    public record RotInputRight : IRotInput
    {
        public float MinAngle => 315;
        public float MaxAngle => 45;
    }

    public record RotInputLeft : IRotInput
    {
        public float MinAngle => 135;
        public float MaxAngle => 225;
    }
    
    public record RotInputAll : IRotInput
    {
        public float MinAngle => 0;
        public float MaxAngle => 360;
    }
}