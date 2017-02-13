﻿namespace TaskCat.Automaton
{
    using Newtonsoft.Json;
    using System;
    using System.IO;

    public class FiniteStateMachine
    {
        /// <summary>
        /// Name of the finite state machine.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Possible variant of the finite state machine.
        /// This is for having different variants of a single finite machine
        /// </summary>
        [JsonProperty("variant")]
        public string Variant { get; set; } = "default";

        public FiniteStateMachine(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Load a finite state machine from a json string.
        /// </summary>
        /// <param name="json">json document of a state machine</param>
        /// <returns></returns>
        public static FiniteStateMachine FromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException(nameof(json));

            var fsm = JsonConvert.DeserializeObject<FiniteStateMachine>(json);
            if (fsm == null)
                throw new NullReferenceException(nameof(fsm));

            return fsm;
        }

        /// <summary>
        /// Load a finite state machine from file path.
        /// </summary>
        /// <param name="filePath">File path where the json state machine is located.</param>
        /// <returns></returns>
        public static FiniteStateMachine FromFile(string filePath)
        {
            using (StreamReader file = File.OpenText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                var fsm = serializer.Deserialize(file, typeof(FiniteStateMachine)) as FiniteStateMachine;
                if (fsm == null)
                    throw new NullReferenceException(nameof(fsm));

                return fsm;
            }
        }
    }
}