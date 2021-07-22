using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;

namespace Matcho.Contents
{
    public class GenreFinder1 : Content<NoCondition>
    {
        public GenreFinder1() : base("ジャンル検索", showAsMenu: true) { }

        private GenreFinderForm1 GenreFinderForm;

        protected override void ConfigureBlock(List<Block> container, string host, NoCondition condition)
        {
            this.GenreFinderForm = new GenreFinderForm1();

            container.Add(this.GenreFinderForm);

            var transition = new GenreFinderTransition1();
            this.GenreFinderForm.AddLinker(transition);

            var transition3 = new GenreFinderTransition3();
            this.GenreFinderForm.AddLinker(transition3);
        }
    }
}
