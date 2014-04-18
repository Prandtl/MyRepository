/*re=/a+b?c/g;
st1='aaabcabc';
if (re.test(st1))
	WSH.echo('match')
else
	WSH.echo('not match');
	
WSH.echo(re.lastIndex);
*/

re1=/a(b+)c(\d+)/g;
st='ab abbbc12 abc57 aabbc3'
var arr = re1.exec(st);
WSH.echo(arr);
while(result=re1.exec(st))
	WSH.echo(result);