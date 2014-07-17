__author__ = 'Vasiliy'
print('ATC number:',end=' ')
atcNum=input()
encoded=''
length=0
currentChar=''
for char in atcNum:
    if (char==currentChar) or (char=='#'):
        length+=1
    else:
        if(length>1):
            encoded+=currentChar
        length=1
        currentChar=char
print(encoded)