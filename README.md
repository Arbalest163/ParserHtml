# ParserHtml
# Описание
Консольное приложение позволяет скачивать произвольную HTML-страницу. Выводит на экран статистику по количеству уникальных слов и сохраняет эту статистику в базу данных SQLite.
# Язык
Приложение написано на языке С#.
# Зависимости
Для корректной работы Приложения необходимо поключить сборки:
> System.Data.SQLite
# Использование
При запуска программы необходимо ввести URL адрес HTML-страницы и нажать ENTER.

Для прмера можно скопировать и вставить: 
> https://simbirsoft.com/

При успешном скачивании в папке создается файл "Test.txt" с исходным кодом HTML-страницы. Далее приожение проводит анализ текста из файла и выводит  на экран статистику по количеству уникальных слов. Данная статистика сохраняется в файл "TestDB.db".

Все ошибки, возникающие при рабое программы сохраняются в Log.txt. Путь сохранения выводится на экран.
