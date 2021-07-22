using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;

namespace Matcho.Contents
{
    public class AreaFinder1 : Content<NoCondition>
    {
        public AreaFinder1() : base("エリア検索", showAsMenu: true) { }

        private AreaFinderForm1 AreaFinderForm;

        protected override void ConfigureBlock(List<Block> container, string host, NoCondition condition)
        {
            this.AreaFinderForm = new AreaFinderForm1();

            container.Add(this.AreaFinderForm);

            this.AreaFinderForm.AddLinker(new AreaFinderTransition1());
            this.AreaFinderForm.AddLinker(new AreaFinderTransition3());
        }
    }
}
