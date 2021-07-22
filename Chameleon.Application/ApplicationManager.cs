using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data;

namespace Chameleon
{
	public class ApplicationManager
	{
		public string Name { get; set; }
        public string Description { get; set; }

        public List<IContent> GetContents()
        {
            var contents = new List<IContent>();

            //現在のコードを実行しているアセンブリを取得する
            Assembly asm = Assembly.GetEntryAssembly();

            //アセンブリで定義されている型をすべて取得する
            Type[] ts = asm.GetTypes();

            //型の情報を表示する
            foreach (Type t in ts)
            {
                //if (t.IsSubclassOf(typeof(Function)))
                if (t.GetInterfaces().Contains(typeof(IContent)))
                {
                    // 引数を持たないコンストラクタを呼び出してインスタンスを作成する
                    IContent content = t.GetConstructor(Type.EmptyTypes).Invoke(null) as IContent;

                    contents.Add(content);
                }
            }

            return contents;
        }

        //public IContent CreateFunction(string functionID, string condition, IDbConnection connection)
        public IContent CreateFunction(string functionID, string condition, string host)
        {
            IContent result = null;
            try
            {
                var constructor = Assembly.GetEntryAssembly().GetType(functionID).GetConstructor(new Type[] { });
                result = constructor.Invoke(new Object[]{}) as IContent;
                result.ConfigureBlock(condition, host);
                //result.Connection = connection;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw ex;
            }

            return result;
        }
    }
}

