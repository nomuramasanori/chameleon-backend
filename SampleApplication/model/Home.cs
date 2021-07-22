using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;
using BusinessClass;


public class Home : Content<NoCondition>
{
    public Home() : base("Home", showAsMenu: false) { }

    protected override void ConfigureBlock(List<Block> container, string host, NoCondition condition)
    {
        container.Add(new Chameleon.Application.Menu(host));
    }
}
