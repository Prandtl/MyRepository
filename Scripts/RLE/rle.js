function code(s)
{
	out='';
	n=1;
	for(i=0;i<=s.length-1;i++)
	{
		if (n>=127)
		{
			out=out+ '#' + String.fromCharCode(n) +s.charAt(i);
			n=1;
			WSH.echo(out + ' 3');
		}
		
		if(i==s.length-1)
		{
			if (n>3||s.charAt(i)=='#')
			{
				out=out+'#'+ String.fromCharCode(n) +s.charAt(i);
				n=1;
				WSH.echo(out+' 4.1');
			}
			else
			{
				WSH.echo(s.slice(i-n+1,n));
				out=out+s.slice(i-n+1,i+1);
				n=1;
				WSH.echo(out+' 4.2');
			}
			break;
		}
		
		if(s.charCodeAt(i)==s.charCodeAt(i+1))
		{
			WSH.echo('I work!');
			n++;
			continue;
		}
		
		if (n>3||s.charAt(i)=='#')
		{
			out=out+'#'+ String.fromCharCode(n) +s.charAt(i);
			n=1;
			WSH.echo(out+' 1');
		}
		else
		{
			WSH.echo(s.slice(i-n+1,n));
			out=out+s.slice(i-n+1,i+1);
			n=1;
			WSH.echo(out+' 2');
		}
	}
	
	WSH.echo(out);
	return out;
}

function decode(s)
{
	out='';
	for (i=0;i<s.length;i++)
	{
		if(s.charAt(i)=='#')
		{
			WSH.echo(s.charCodeAt(i+1)+' 1');
			WSH.echo(s.slice(i,i+2)+' 1');
			for(j=0;j<s.charCodeAt(i+1);j++)
			{
				out=out+s.charAt(i+2);
			}
			i+=2;
			
		}
		else
		{
			WSH.echo(s.charAt(i)+' 2');
			out=out+s.charAt(i);
		}
	}
	return out;
}
mode = WSH.Arguments(0);
fso = new ActiveXObject("Scripting.FileSystemObject");
fh = fso.OpenTextFile(WSH.Arguments(1));
s = fh.ReadAll();
fh.Close();

if (mode=='code'){
	s=code(s);
	fh = fso.OpenTextFile(WSH.Arguments(2), 2, true);
	fh.WriteLine(s);
	fh.Close();
	WSH.echo(s);
}
else
{
s=decode(s);
fh = fso.OpenTextFile(WSH.Arguments(2), 2, true);
fh.WriteLine(s);
fh.Close();
WSH.echo(s);
}

