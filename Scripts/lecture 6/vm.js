var mem = new Array();
var fso = new ActiveXObject('Scripting.FileSystemObject');
var text_prog= fso.OpenTextFile('first.xpp');
WSH.echo('Opened');
var s='';
while(!text_prog.AtEndOfStream)
{
	s+=text_prog.ReadLine()+' ';
}
s+='exit';
mem=s.split(' ');
var ip=0;
for(var count=0;count<mem.length;count++)
{
	WScript.echo('В ячейке ',count,' хранится ',mem[count]);
}

while(mem[ip]!='exit')
{
	switch(mem[ip])
	{
		case 'input':
			WScript.Echo('Введи значение')
			mem[mem[ip+1]]=parseFloat(WScript.StdIn.ReadLine())
			ip+=2
			break
		case 'output':
			WScript.Echo(mem[mem[ip+1]])
			ip+=2
			break
		case 'add':
			mem[mem[ip+3]]=mem[mem[ip+1]]+mem[mem[ip+2]]
			ip+=4
			break
		case 'exit':
			WScript.Quit()
	}
}