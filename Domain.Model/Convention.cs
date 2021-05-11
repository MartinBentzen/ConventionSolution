using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Model
{
    public class Convention
    {
        public Guid Id { get; private set; }
        public string Topic { get; private set; }
        public string Name { get; private set; }
        public int MaxCap { get; private set; }
        public int NumberOfParticipants { get; private set; }
        public BaseSpeaker Speaker { get; private set; }
        private Convention(Guid id, string name, string topic, int maxCap, int numberOfParticipants, BaseSpeaker speaker)
        {
            MaxCap = maxCap;
            NumberOfParticipants = numberOfParticipants;
            Speaker = speaker;
            Id = id;
            Name = name;
            Topic = topic;
        }
        public static Convention Create(Guid id, string name, string topic, int maxCap, int numberOfParticipants, BaseSpeaker speaker)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (topic == null) throw new ArgumentNullException(nameof(topic));
            if (speaker == null)
                speaker = NotAvaliableSpeaker.Create();

            return new Convention(id, name, topic, maxCap, numberOfParticipants, speaker);
        }

        public bool IsFull()
        {
            return MaxCap == NumberOfParticipants;
        }

        public void AddSpeaker(Speaker speaker)
        {
            Speaker = speaker;
        }

        public bool HasSpeaker()
        {
            var speaker = Speaker as Speaker;
            if (speaker == null)
            {
                return false;
            }

            return true;
        }
    }
}
