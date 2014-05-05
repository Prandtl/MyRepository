var orders = new Array();
var mem =new Array();
var nesting=new Array();

var fso = new ActiveXObject('Scripting.FileSystemObject');
var text_prog= fso.OpenTextFile(WSH.Arguments(0));
WSH.echo('Opened');

while(!text_prog.AtEndOfStream)
{
	s=text_prog.ReadLine();
	s=s.split(' ')
	orders.push(s)
}

i=0;

while(orders[i]!='exit')
{
	order=orders[i]
	switch(order[0])
	{
		case 'input':
			WSH.echo('Input, please')
			mem[order[1]]=parseFloat(WScript.StdIn.ReadLine())
			i++
			break
		case 'output':
			WSH.echo(mem[order[1]])
			i++
			break
		case 'add':
			mem[order[3]]=mem[order[1]]+mem[order[2]]
			i++
			break
		case 'increase':
			mem[order[1]]=parseFloat(mem[order[1]])+1
			i++
			break
		case 'mult':
			mem[order[3]]=mem[order[1]]*mem[order[2]]
			i++
			break
		// case 'whileDifferent':
			// if(mem[order[1]]==mem[order[2]])
			// {
				// while(order[i]!='end')
					// i++
				// i++
				// break
			// }
			// WSH.echo('while isnt working')
			// i++
			// break
		case 'jump':
			i=order[1]-1
			break
		case 'ifDifferent':
			if(mem[order[1]]==mem[order[2]])
			{
				while(orders[i]!='end')
					i++
				i++
				break
			}
			i++
			break
		case 'exit':
			WSH.echo('Exit!')
			WScript.Quit()
		case 'put':
			mem[order[1]]=order[2]
			i++
			break
		case 'clone':
			mem[order[2]]=mem[order[1]]
			i++
			break
		case 'end':
			i++
			break
		default:
			i++
			break
	}
}

WSH.echo('Exit!')