﻿namespace SenseIt.Web.ViewModels.Reviews
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SenseIt.Web.ViewModels.AppServices;

    using static SenseIt.Common.GlobalConstants.Review;

    public class CreateReviewInputModel
    {
        public AppServiceInListViewModel AppService { get; set; }

        public string PosterById { get; set; }

        public int AppServiceId { get; set; }

        [Required]
        [MinLength(ContentMinLength)]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Range(1.0, 5.0)]
        public double Rating { get; set; }
    }
}
