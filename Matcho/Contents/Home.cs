using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;
using Matcho.Contents;

public class Home : Content<NoCondition>
{
    public Home() : base("Home", showAsMenu: false) { }

    private FreeWordFinderForm FreeWordFinderForm;

    protected override void ConfigureBlock(List<Block> container, string host, NoCondition condition)
    {
        this.FreeWordFinderForm = new FreeWordFinderForm();

        container.Add(this.FreeWordFinderForm);
        container.Add(new Chameleon.Application.Menu(host));

        var transition = new FreeWordFinderTransition();
        this.FreeWordFinderForm.AddLinker(transition);
    }
}
