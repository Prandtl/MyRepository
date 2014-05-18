mode = WSH.Arguments(0);
fso = new ActiveXObject("Scripting.FileSystemObject");
fh = fso.OpenTextFile('in.txt');
s = fh.ReadAll();
// fh.Close();
// fh = fso.OpenTextFile('out.txt',2);
// fh.WriteLine(s);
// fh.Close();

function MyFloat(sign,metric,mantiss)
{
	this.sign=sign;
	this.metric=metric;
	this.mantiss=mantiss;
}

function FromStringToFloat(s)
{
	if (s.charAt(0)=='-')
	{
		sign=1
		s=s.slice(1)
	}
	else
		sign=0
		
	s=s.split('.')
	number=parseInt(s[0]).toString(2)
	
	if(s.length<2)
		end=0
	else
		end=parseInt(s[1])
	
	if(number!='0')
	{
		if (number.length<24)
		{
			metric=127
			metric+=number.length-1
			i=0
			while(number.length<24)
			{
				end=end*2
				end=end.toString()
				if (end.charAt(0)==1)
				{
					number+='1'
					end=end.substr(1)
					metric+=i
					i=0
				}
				else
				{
					number+='0'
					i++
				}
				end=parseInt(end)
			}
			mantiss=number.substr(1);
		}
		else
		{
			nLength=number.length//изменение метрики если больше X(найти X) то бесконечность
			number=number.slice(0,23)
			metric+=
		}
	}
	else
	{
		number=''
		while(metric>0&&number.length<1)
		{
			end=end*2
			end=end.toString()
			if (end.charAt(0)==1)
			{
				number+='1'
				end=end.substr(1)//уменьшаем метрику пока не появится единичка
				metric+=i
				i=0
			}
			else
			{
				number+='0'
				i++
			}
			end=parseInt(end)
			//уменьшаем метрику пока у мантиссы порядок меньше необходимого либо пока метрика не ноль(в таком случае денормализация(Спаси нас Господь))
		}
	}
	metric=parseInt(metric).toString(2)
	while(metric.length<8)
		metric='0'+metric
	WSH.echo(sign+' '+metric+' '+mantiss);
}

FromStringToFloat(s)
fh.Close();