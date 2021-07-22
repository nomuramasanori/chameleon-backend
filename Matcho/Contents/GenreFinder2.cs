using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;

namespace Matcho.Contents
{
    public class GenreFinder2 : Content<TrainerListCondition>
    {
        public GenreFinder2() : base("ジャンル検索", showAsMenu: false) { }

        private GenreFinderForm2 GenreFinderForm;

        protected override void ConfigureBlock(List<Block> container, string host, TrainerListCondition condition)
        {
            this.GenreFinderForm = new GenreFinderForm2(condition);

            container.Add(this.GenreFinderForm);

            var transition = new GenreFinderTransition2(condition);
            this.GenreFinderForm.AddLinker(transition);
        }
    }
}
