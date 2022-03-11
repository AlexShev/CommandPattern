namespace Laba3
{
    class ConsoleInteractor
    {
        private readonly Editor _editor;

        public ConsoleInteractor(string path)
        {
           _editor = new Editor(new TextFileWorker(path).Read(), path);
        }

        enum MenuPoint
        {
            error = -1,
            exit,
            showMenu,
            showText,
            insert,
            pushBack,
            removeAt,
            replaceAt,
            undo,
            redo,
            save,
            delete,
        }

        private static readonly SortedDictionary<MenuPoint, string> MENU = new()
        {
            { MenuPoint.showMenu, "показать меню" },
            { MenuPoint.showText, "показать текст" },
            { MenuPoint.insert, "добавить строку в указанную позицию" },
            { MenuPoint.removeAt, "удалить строку из указанной позиции" },
            { MenuPoint.replaceAt, "заменить строку в указанной позиции" },
            { MenuPoint.undo, "undo" },
            { MenuPoint.redo, "redo" },
            { MenuPoint.save, "сохранить изменения" },
            { MenuPoint.delete, "удалить всё" },
            { MenuPoint.exit, "завершить работу" },
            { MenuPoint.pushBack, "добавить строку в конец"}
        };

        private static void ShowMenu()
        {
            foreach (var item in MENU)
            {
                Console.WriteLine($"{(int)item.Key}: {item.Value}");
            }
        }

        private static string? ReadLine()
        {
            Console.Write(">> ");

            return Console.ReadLine();
        }

        private static int ReadInt()
        {
            string? s = ReadLine();

            if (int.TryParse(s, out int code))
            {
                return code;
            }
            else
                return -1;
        }

        private static bool Confirmation()
        {
            Console.WriteLine("Для подтверждения нажмите 1");

            if (Console.ReadKey(true).Key == ConsoleKey.D1)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Команда отменена");
                return false;
            }
        }


        public void Run()
        {
            try
            {
                var seporator = new string('=', 20);

                MenuPoint operation;
                ShowMenu();
                do
                {
                    operation = (MenuPoint)ReadInt();

                    switch (operation)
                    {
                        case MenuPoint.exit:
                            if (!Confirmation())
                            {
                                operation = MenuPoint.error;
                            }
                            break;
                        case MenuPoint.showMenu:
                            ShowMenu();
                            break;
                        case MenuPoint.showText:
                            ShowText(_editor, seporator);
                            break;
                        case MenuPoint.insert:
                            Insert(_editor);
                            break;
                        case MenuPoint.pushBack:
                            PushBack(_editor);
                            break;
                        case MenuPoint.removeAt:
                            RemoveAt(_editor);
                            break;
                        case MenuPoint.replaceAt:
                            ReplaceAt(_editor);
                            break;
                        case MenuPoint.undo:
                            Undo(_editor);
                            break;
                        case MenuPoint.redo:
                            Redo(_editor);
                            break;
                        case MenuPoint.save:
                            Save(_editor);
                            break;
                        case MenuPoint.delete:
                            Delete(_editor);
                            break;
                        default:
                            Console.WriteLine("Такой команды нет!!!");
                            break;
                    }

                    Console.WriteLine();
                } while (operation != MenuPoint.exit);


                if (!_editor.IsSaved)
                {
                    Console.WriteLine("У вас есть не сохранённые данные.");
                    Console.WriteLine("Вы хотите их сохранить?");
                    if (Confirmation())
                    {
                        _editor.Save();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Что-то пошло не так");
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        private static void ShowText(Editor editor, string seporator)
        {
            Console.WriteLine(seporator);
            Console.WriteLine(editor.Text);
            Console.WriteLine(seporator);
        }

        private static void Delete(Editor editor)
        {
            if (Confirmation())
            {
                editor.Delete();
                Console.WriteLine("Всё удалено");
            }
        }

        private static void Save(Editor editor)
        {
            if (Confirmation())
            {
                editor.Save();
                Console.WriteLine("Изменения сохранены");
            }
        }

        private static void Redo(Editor editor)
        {
            if (editor.CanRedo())
            {
                editor.Redo();
                Console.WriteLine("Действие восстановлено");
            }
            else
            {
                Console.WriteLine("Нет возможности восстановить действие");
            }
        }

        private static void Undo(Editor editor)
        {
            if (editor.CanUndo())
            {
                editor.Undo();
                Console.WriteLine("Действие отменено");
            }
            else
            {
                Console.WriteLine("Нет возможности отменить действие");
            }
        }

        private static void ReplaceAt(Editor editor)
        {
            Console.WriteLine($"Введите место редактирования в пределе от 0 до {editor.Size}");
            int i = ReadInt();

            if (editor.LineAt(i) != null)
            {
                Console.WriteLine(editor.LineAt(i));

                Console.WriteLine("Введите строку");
                editor.ReplaceAt(i, ReadLine());
            }
            else
            {
                Console.WriteLine("Строка не найдена!");
            }
        }

        private static void RemoveAt(Editor editor)
        {
            Console.WriteLine($"Введите место удаления в пределе от 0 до {editor.Size}");

            editor.RemoveAt(ReadInt());

            if (!editor.IsComplete)
            {
                Console.WriteLine("Неправильное место удаления, отмена команды");
            }
        }

        private static void Insert(Editor editor)
        {
            Console.WriteLine($"Введите место вставки в пределе от 0 до {editor.Size}");
            int i = ReadInt();

            Console.WriteLine("Введите строку");
            editor.Insert(i, ReadLine());

            if (!editor.IsComplete)
            {
                Console.WriteLine("Неправильное место вставки, отмена команды");
            }
        }

        private static void PushBack(Editor editor)
        {
            Console.WriteLine("Введите строку");
            editor.Insert(editor.Size, ReadLine());

            if (!editor.IsComplete)
            {
                Console.WriteLine("что-то пошло не так!");
            }
        }
    }
}