# VR-simulator (Event System UI)
 Как пользоваться системой событий:
 1) Нажимаем правую кнопку мыши -> Create -> Event System -> Node.
 2) Настраиваем узел, задав gameObject и его скрипт, после выбираем условия перехода (одно из публичных свойств выбранного класса) к следующему узлу.
 ![1]()
 3) Создаем еще один узел и также настраиваем его, но теперь задаем ему функцию активации (один из публичных методов выбранного класса).
 ![2]()
 4) Открываем редактор, выбраов Windo -> EventSystem.
 5) Нажимаем правую кнопку мыши в открывшемся окне и выбираем созданные узлы, для их отображения в окне.
 6) Соединяем узел условия с узлом действия.
 ![3]()
 7) Нажимаем Ctrl+S для сохранения. Теперь при запуске игры, если условие узла выполняется, то происходит переход к следующему узлу и выполняется его действие.
 Примечания: Количество условий и действий в узле не ограничено. Возможность соединять многие к одному и одному ко многим узлов. 
