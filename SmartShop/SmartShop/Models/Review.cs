﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartShop.Models
{
    public class Review
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("created_at")]
        public DateTime Date { get; set; }
        [JsonProperty("rating")]
        double rating;
        public string Rating
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();

                if (rating > 0)
                {
                    int ratingCell = (int)rating;
                    int remainder = 5 - ratingCell;

                    if (ratingCell > 0)
                        stringBuilder.Insert(0, "★", ratingCell);

                    if (remainder > 0)
                        stringBuilder.Insert(ratingCell, "☆", remainder);

                }

                return stringBuilder.ToString();
            }
        }
    }
}
