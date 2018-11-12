# AutoCreatorCourtOrder
Программа создает судебный приказ на основе заявления о вынесении судебного приказа.
На текущий момент программа работает только с заявлением из налоговой, возможно позже выложу пример заявления с удаленными личными данные.




Ниже readme для пользователя:

При создании шаблона судебного приказа используются следующие ключевые слова:  
#FULLNAME# - заменяется на ФИО  
#DATEOFBIRTH# - заменяется на дату рождения  
#PLACEOFBIRTH# - заменяется на место рождения  
#ADDRESS# - заменяется на адрес  
#INDIVIDUALTAXNUMBER# - заменяется на ИНН  
#DEBTSTRUCTURE# - заменяется на текст описывающий задолженность  
#GOSPOSHLINA# - заменяется на сумму госпошлины  

Ключевые слова могут использоваться в тексте несколько раз, 
каждое ключевое слово будет заменено на соотвествующие ему данные.
Ключевые слова должны использоваться без изменений в верхнем регистре
