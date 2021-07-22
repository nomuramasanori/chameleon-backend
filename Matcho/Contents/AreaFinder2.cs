using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;

namespace Matcho.Contents
{
    public class AreaFinder2 : Content<TrainerListCondition>
    {
        public AreaFinder2() : base("エリア検索", showAsMenu: false) { }

        private AreaFinderForm2 AreaFinderForm;

        protected override void ConfigureBlock(List<Block> container, string host, TrainerListCondition condition)
        {
            this.AreaFinderForm = new AreaFinderForm2(condition);

            container.Add(this.AreaFinderForm);

            this.AreaFinderForm.AddLinker(new AreaFinderTransition2(condition));
        }
    }
}
