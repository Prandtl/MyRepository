mode = WSH.Arguments(0);//дедлайн - 26 марта
fso = new ActiveXObject("Scripting.FileSystemObject");
fh = fso.OpenTextFile(WSH.Arguments(1));
s = fh.ReadAll();
fh.Close();
if (mode=='code'){
	out='';
	for (var i=0;i<s.length;i++)
		{
			if (s[i]==s[i+1]) {
				n++;
				continue;
			}
			if (n>3)
			{
				if(n>=255)
				{
					c+
				}
			}
		}
		if (s[i]) 
	}
	fh = fso.OpenTextFile(WSH.Arguments(2), 2, true);
	fh.WriteLine(s);
	fh.Close();
	WSH.echo(s);
}
else
{

fh = fso.OpenTextFile(WSH.Arguments(2), 2, true);
fh.WriteLine(s);
fh.Close();
WSH.echo(s);
}

