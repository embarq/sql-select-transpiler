# Лабораторный проект по дисциплине "Формальные грамматики"
**ТЗ**: Разработать приложение для работы с базой данных посредством `SQL` запросов
## Требования
1. Простейшая модель базы данных
  - Управление таблицами
    - Создание
    - Удаление
    - Модификация
    - Импорт
    - Экспорт
2. Реализация `SQL`-инструкции выборки `SELECT`

3. Простейший `GUI`
  - Форма ввода запроса
  - Форма вывода результата запроса

## Спецификация
- Функционал декларируется в пределах операций
```sql 
SELECT ... FROM ... GROUP BY ... WHERE ... HAVING ... ORDER BY
```
- Имя таблицы состоит из совокупности букв и цифр, начинающаяся с буквы. Длина не более 8 символов
  - Описание таблицы можно ограничить только перечнем атрибутов и их типов данных
- Имя атрибута состоит из совокупности букв, цифр, символов «\_» и «-», начинающийся с буквы или символа «\_». Длина не более 16 символов
  - Проверяется уникальность имен атрибутов и их несовпадение с именем таблицы
  - При использовании расчетных полей и функций проверяется наличие использованных атрибутов в таблице
  - При использовании функций проверяется корректность их применения - соответствует ли этой функции тип данных (*воможно ли с этим типом данных выполнять арифметические операции*)
- Условие выборки строк, следующеe после фразы `HAVING`, является логическим выражением и предполагает любую глубину вложения скобок, при этом проверяется соответствие между скобками, открывающие выражение и его закрывающие
- Грамматика оператора расширяется за счет инструкции `ORDERBY`, в том числе с опциями `ASC` и `DESC`
- После инструкций `GROUP BY` и `HAVING` могут быть атрибуты только предварительное заданной таблицы
- После инструкции `ORDERBY` могут быть атрибуты только предварительное заданной таблицы, имена расчетных полей не передаются, поэтому для составления по ним нужно снова записывать функции, применяемые в инструкции `SELECT`
