function arrayVisualise(a){
WSH.echo('-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_')
for(i in a)
	WSH.echo(a[i])
WSH.echo('------------------------------')
}

function solve(a){
	i=0;
	stack=new Array()
	while(i<a.length)
	{
		e=a[i]
		
		if (typeof e=='number')
		{
			stack.push(e)
		}
		else
		{
			second=stack.pop();
			first=stack.pop();
			switch(e)
			{
				case '+':
					stack.push(first+second)
					break
				case '*':
					stack.push(first*second)
					break
				case '-':
					stack.push(first-second)
					break
				case '/':
					stack.push(first/second)
					break
				case '^':
					stack.push(Math.pow(first,second))
					break
			}
		}
		i++
	}
	return stack.pop()
}

function stringToArray(a){
	arr=a.split(' ');
	b=new Array();
	for(i in arr)
	{
		if (arr[i].charAt(0)<'9'&&arr[i].charAt(0)>'0')
			b.push(parseFloat(arr[i]))
		else
			b.push(arr[i]);
	}
	return b;
}

function toRPN(a){
	priority=new Array()
	priority['^']=2
	priority['*']=1
	priority['/']=1
	priority['+']=0
	priority['-']=0
	priority['(']=-1
	
	i=0
	stack=new Array()
	res=new Array()
	while(i<a.length)
	{
		WSH.echo()	
		if (typeof a[i]=='number')
		{
			res.push(a[i])
			WSH.echo('got a number here '+a[i])
			WSH.echo()
		}
		else
		{
			if(a[i]=='(')
			{
				stack.push(a[i])
				WSH.echo('got an opening bracket here '+a[i])
				WSH.echo()
			}
			else
			{
				WSH.echo("I want an operator!")
				if(a[i]==')')
				{
					while(stack[stack.length-1]!='(')
						res.push(stack.pop())
					stack.pop()
					WSH.echo('got a closing bracket here '+a[i])
					WSH.echo()
				}
				else
				{
					while(stack.length>0&&priority[stack[stack.length-1]]>priority[a[i]])
						res.push(stack.pop())
					stack.push(a[i])
					WSH.echo('Aw, maaan, an operator down here '+a[i])
					WSH.echo()
				}
			}
		}
		i++
		WSH.echo('res: '+res)
		WSH.echo('stack: '+stack)
	}
	while(stack.length>0)
			res.push(stack.pop())
	return res
}

a=stringToArray('12 * 11 + 1 / 44 + 56 - 3')
b=toRPN(a)
WSH.echo(b)
WSH.echo(solve(b))
