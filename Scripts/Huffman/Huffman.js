
function node(name,fr,used,link,code)
{
	this.name=name;
	this.fr=fr;
	this.used=used;
	this.link=link;
	this.code=code;
}


tree=new Array();
str = WSH.StdIn.ReadLine();
alph=new Array();
for(i=0;i<str.length;i++)
	alph[str.charAt(i)]=0
for(i=0;i<str.length;i++)
	alph[str.charAt(i)]++
for(i in alph)
{
	n=new node(i,alph[i],0,null,'');
	tree.push(n);
}

	
length=tree.length;

for(j=0;j<length-1;j++)
{
	fr1 = str.length;
	num1 = 0;
	for(i=0;i<tree.length;i++)
	{
		if((tree[i].fr<fr1)&&(tree[i].used!=1))
		{
			fr1 = tree[i].fr;
			num1 = i;
		}
	}
	tree[num1].used = 1;
	tree[num1].code = 0;
	tree[num1].link = tree.length;
	fr2 = str.length;
	num2 = 0;
	for(i=0;i<tree.length;i++)
	{
		if((tree[i].fr<fr2)&&(tree[i].used!=1))
		{
			fr2 = tree[i].fr;
			num2 = i;
		}
	}
	tree[num2].used = 1;
	tree[num2].code = 1;
	tree[num2].link = tree.length;
	tree.push(new node(tree[num1].name + tree[num2].name, tree[num1].fr + tree[num2].fr, 0, null, ''));

}

code = new Array();
for(i=0;i<length;i++)
{
	j = i;
	code[tree[j].name] = '';
	while(tree[j].link)
	{
		code[tree[i].name] = tree[j].code + code[tree[i].name];
		j = tree[j].link;
	}
}

if (tree.length==1)
{
	code[tree[0].name]=0;
}

for(i in code)
	WSH.echo(i,' ',code[i]);