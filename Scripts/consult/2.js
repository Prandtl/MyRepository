/*
re1=/a+b/g;
re2=/b+a/g;
st='aabbaba';
re1.exec(st);
WSH.echo(re1.lastIndex);
WSH.echo(RegExp.lastMatch);
re2.exec(st);
WSH.echo(re2.lastIndex);
WSH.echo(re1.lastIndex);
WSH.echo(RegExp.lastIndex);
WSH.echo(RegExp.lastMatch);
*/
var src = "�� ������: <I>� �����</I> � �������: <I>�� ��������</I>.";
var res = src.match(/<i>.*?<\/i>/i);
WSH.echo(res);
var res = src.match(/<i>.*?<\/i>/ig);
WSH.echo(res);

re=/ (dean)(.| )/i;
