using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;

namespace Checkers
{
    [TestClass]
    public class ValidatorTests
    {
        Checker[,] GetMapFrom(string filename)
        {
            var map = new Checker[8, 8];
            var file = File.ReadAllLines("Maps\\" + filename);
            for (var i = 0; i < 8; i++)
                for (var j = 0; j < 8; j++)
                    switch (file[i][j])
                    {
                        case '1':
                            map[j, i] = new Checker(Color.White, false);
                            break;
                        case '2':
                            map[j, i] = new Checker(Color.Black, false);
                            break;
                        case '3':
                            map[j, i] = new Checker(Color.White, true);
                            break;
                        case '4':
                            map[j, i] = new Checker(Color.Black, true);
                            break;
                        default:
                            break;
                    }
            return map;
        }
        [TestMethod]
        public void SomeStrange() // привет епт
        {
            var field = GetMapFrom("Tests88.txt");
            var validator = new Validator();
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 7), new Point(7, 0))); // дамка просто делает привязный ход и рубит другую дамку
            validator.IsCorrectMove(moves, field, Color.Black); //твой забаганный валидатор получает этот ход, поле и все такое.
            var newField = GetMapFrom("Tests89.txt"); //беру карту которая должна быть после хода (с дамкой)
            Assert.AreEqual(Serializer.FieldToString(newField), Serializer.FieldToString(field)); //обнаруживаю, что дамки ниха нет :С
        }
        [TestMethod]
        public void SomeStrange2() // эта фишка только если рубишь из угла. если ближе -- такого нет
        {
            var field = GetMapFrom("Tests90.txt");
            var validator = new Validator();
            var moves = new List<Move>();
            moves.Add(new Move(new Point(7, 7), new Point(0, 0))); 
            validator.IsCorrectMove(moves, field, Color.White);
            var newField = GetMapFrom("Tests91.txt"); 
            Assert.AreEqual(Serializer.FieldToString(newField), Serializer.FieldToString(field));
        }
        [TestMethod]
        public void SomeStrange3() // так же это только из угла в угол. если рубишь ближе -- все хорошо
        {
            var field = GetMapFrom("Tests92.txt");
            var validator = new Validator();
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 7), new Point(7, 0)));
            validator.IsCorrectMove(moves, field, Color.Black);
            var newField = GetMapFrom("Tests93.txt");
            Assert.AreEqual(Serializer.FieldToString(newField), Serializer.FieldToString(field));
        }
        //Итого: Этот баг для обоих цветов. Этот баг распространяется только если рубящая фигура -- дамка.
        //Пешка ли, дамка ли та, кого бьют -- не важно. Где находится вражеская фигура на диагонали -- не важно.
        //При рубке дамкой из одного угла в другой через всю диагональ -- обе фигуры исчезают. 
        //Если дамка рубит не в последнюю клетку -- все хорошо, если дамка рубит не из последней клетки -- все хорошо.
        [TestMethod]
        public void HashTest()
        {
            var moves = new HashSet<Move>();
            var field = GetMapFrom("Tests12.txt");
            var valid = new Validator();
            moves = valid.GetBindingMoves(field, Color.White);
            Assert.AreEqual(0, moves.Count);
        }
        [TestMethod]
        public void CheckCorrectMap()
        {
            var field = GetMapFrom("Tests01.txt");
            var a = field[3, 3].Color == Color.White;
            var b = field[2, 2].Color == Color.Black;
            var c = field[4, 4] == null;
            Assert.AreEqual(a && b && c, true);
        }
        void Test(List<Move> moves, Color color, bool answer, string mapname)
        {
            var validator = new Validator();
            var field = GetMapFrom(mapname);
            if (!answer)
                try 
                { 
                    validator.IsCorrectMove(moves, field, color); 
                    Assert.Fail(); 
                }
                catch (NotImplementedException) { }
            else
                validator.IsCorrectMove(moves, field, color);

        }
        [TestMethod]
        public void MustAttackBlackFalse()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(2, 2), new Point(1, 3)));
            Test(moves, Color.Black, false, "Tests01.txt");
        }
        [TestMethod]
        public void DoubleMoveThroughOneChecker()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(2, 2), new Point(4, 4)));
            moves.Add(new Move(new Point(4, 4), new Point(2, 2)));
            Test(moves, Color.Black, false, "Tests01.txt");
        }
        [TestMethod]
        public void DoubleMoveAfterAttack() // второй ход после атаки
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(2, 2), new Point(4, 4)));
            moves.Add(new Move(new Point(4, 4), new Point(5, 5)));
            Test(moves, Color.Black, false, "Tests01.txt");
        }
        [TestMethod]
        public void MustAttackBlackTrue()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(2, 2), new Point(4, 4)));
            Test(moves, Color.Black, true, "Tests01.txt");
        }
        [TestMethod]
        public void MustAttackWhiteTrue()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(3, 3), new Point(1, 1)));
            Test(moves, Color.White, true, "Tests01.txt");
        }
        [TestMethod]
        public void MoveNotYourOwn() //пробую сходить не своей фигурой
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(3, 3), new Point(1, 1)));
            Test(moves, Color.Black, false, "Tests01.txt");
        }
        [TestMethod]
        public void QueenMustAttackWhiteTrue() //пробую верно срубить королевой
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(7, 7)));
            Test(moves, Color.White, true, "Tests02.txt");
        }
        [TestMethod]
        public void QueenMustAttackBlackTrue()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(6, 6), new Point(0, 0)));
            Test(moves, Color.Black, true, "Tests02.txt");
        }
        [TestMethod]
        public void QueenMustAttackWhiteFalse()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(4, 4)));
            Test(moves, Color.White, false, "Tests02.txt");
        }
        [TestMethod]
        public void QueenMustAttackBlackFalse()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(6, 6), new Point(5, 7)));
            Test(moves, Color.Black, false, "Tests02.txt");
        }
        [TestMethod] 
        public void NormalMoveWhite()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(6, 6), new Point(5, 5)));
            Test(moves, Color.White, true, "Tests03.txt");
        }
        [TestMethod]
        public void DoubleMove() //простой двойной ход
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(6, 6), new Point(5, 5)));
            moves.Add(new Move(new Point(5, 5), new Point(4, 4)));
            Test(moves, Color.White, false, "Tests03.txt");
        }
        [TestMethod]
        public void NormalMoveBlack()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(0, 2)));
            Test(moves, Color.Black, true, "Tests03.txt");
        }
        [TestMethod]
        public void MoveBackBlackFalse()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(0, 0)));
            Test(moves, Color.Black, false, "Tests03.txt");
        }
        [TestMethod]
        public void MoveBackWhiteFalse()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(6, 6), new Point(5, 7)));
            Test(moves, Color.White, false, "Tests03.txt");
        }
        [TestMethod]
        public void WrongMoveWhite()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(6, 6), new Point(4, 4)));
            Test(moves, Color.White, false, "Tests03.txt");
        }
        [TestMethod]
        public void WrongMoveBlack()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(6, 6), new Point(6, 7)));
            Test(moves, Color.Black, false, "Tests03.txt");
        }
        [TestMethod] 
        public void NormalQueenMoveWhite()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(7, 4), new Point(5, 6)));
            Test(moves, Color.White, true, "Tests04.txt");
        }
        [TestMethod]
        public void NormalQueenMoveBlack()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(7, 0), new Point(0, 7)));
            Test(moves, Color.Black, true, "Tests04.txt");
        }
        [TestMethod]
        public void WrongQueenMoveBlack()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(7, 0), new Point(7, 7)));
            Test(moves, Color.Black, false, "Tests04.txt");
        }
        [TestMethod]
        public void WrongQueenMoveWhite()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(7, 4), new Point(6, 4)));
            Test(moves, Color.White, false, "Tests04.txt");
        }
        [TestMethod]
        public void WrongQueenMoveWhite2()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(7, 7)));
            Test(moves, Color.White, false, "Tests05.txt");
        }
        [TestMethod]
        public void WrongQueenMoveWhite3()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(5, 5)));
            Test(moves, Color.White, false, "Tests05.txt");
        }
        [TestMethod]
        public void RightQueenMoveWhite()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(2, 2)));
            Test(moves, Color.White, true, "Tests05.txt");
        }
        [TestMethod]
        public void WrongKill()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(2, 2), new Point(4, 4)));
            Test(moves, Color.White, false, "Tests06.txt");
        }
        [TestMethod]
        public void WrongKill2()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(3, 3), new Point(1, 1)));
            Test(moves, Color.White, false, "Tests06.txt");
        }
        [TestMethod]
        public void QueenAttack1()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(3, 3)));
            Test(moves, Color.White, true, "Tests07.txt");
        }
        [TestMethod]
        public void QueenAttack2()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(5, 5)));
            Test(moves, Color.White, true, "Tests07.txt");
        }
        [TestMethod]
        public void QueenAttack3()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(7, 7)));
            Test(moves, Color.White, true, "Tests07.txt");
        }
        [TestMethod]
        public void MultiMoveCheckTrue()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(3, 3)));
            moves.Add(new Move(new Point(3, 3), new Point(5, 5)));
            Test(moves, Color.White, true, "Tests08.txt");
        }
        [TestMethod]
        public void MultiMoveCheckFalse()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(3, 3)));
            moves.Add(new Move(new Point(3, 3), new Point(2, 4)));
            Test(moves, Color.White, false, "Tests08.txt");
        }
        [TestMethod]
        public void QueenMultiCheckTrue()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(4, 4)));
            moves.Add(new Move(new Point(4, 4), new Point(2, 6)));
            Test(moves, Color.White, true, "Tests09.txt");
        }
        [TestMethod]
        public void QueenMultiCheckFalse() //после двух ходов обязательных квина делает третий ход(wrong)
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(4, 4)));
            moves.Add(new Move(new Point(4, 4), new Point(2, 6)));
            moves.Add(new Move(new Point(2, 6), new Point(1, 7)));
            Test(moves, Color.White, false, "Tests09.txt");
        }
        [TestMethod]
        public void WrongMoveNullReference() //не понял каким образом кидается нул референс. простой попыткой сходить из нула вызвать эксепшн не удалось.
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(4, 4)));
            moves.Add(new Move(new Point(4, 4), new Point(2, 6)));
            moves.Add(new Move(new Point(4, 4), new Point(1, 7)));
            Test(moves, Color.White, false, "Tests09.txt");
        }
        [TestMethod]
        public void GeneratingQueenTrue() // при достижении 0 строки белая фишка должна становиться дамкой, после чего обязана атаковать если есть возможность.
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(7, 2), new Point(5, 0)));
            moves.Add(new Move(new Point(5, 0), new Point(0, 5)));
            Test(moves, Color.White, true, "Tests10.txt");
        }
        [TestMethod]
        public void GeneratingQueenFalse() // обязаны атаковать став дамкой.
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(7, 2), new Point(5, 0)));
            moves.Add(new Move(new Point(5, 0), new Point(7, 2)));
            Test(moves, Color.White, false, "Tests10.txt");
        }
        [TestMethod]
        public void DoubleMoveBlack()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(2, 2)));
            moves.Add(new Move(new Point(3, 1), new Point(4, 2)));
            Test(moves, Color.Black, false, "Tests11.txt");
        }
        [TestMethod]
        public void DoubleMoveBlackTrue()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(3, 1), new Point(4, 2)));
            Test(moves, Color.Black, true, "Tests11.txt");
        }
        [TestMethod]
        public void DrawMoveTest()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 5), new Point(1, 4)));
            Test(moves, Color.White, true, "Tests12.txt");
        }
        [TestMethod]
        public void MustDoubleAttack()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(1, 1), new Point(3, 3)));
            Test(moves, Color.White, false, "Tests08.txt");
        }
        [TestMethod]
        public void QueenKillThemAll()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(6, 6)));
            Test(moves, Color.White, false, "Tests13.txt");
        }
        [TestMethod]
        public void QueenKillThemAll2()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(7, 7)));
            Test(moves, Color.White, false, "Tests13.txt");
        }
        [TestMethod]
        public void QueenPrettyGirl()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(1, 1)));
            Test(moves, Color.White, true, "Tests13.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(3, 3)));
            Test(moves, Color.White, false, "Tests13.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate2()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(3, 3)));
            Test(moves, Color.White, false, "Tests14.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate3()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(1, 1)));
            Test(moves, Color.White, false, "Tests15.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate4()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(3, 3)));
            Test(moves, Color.White, true, "Tests15.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate5() //непонятная хунта, почему-то рубит через все поле -- зеленый свет, а в 7 тесте норм палит парашку.
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(7, 7)));
            Test(moves, Color.White, false, "Tests15.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate6()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(1, 1)));
            Test(moves, Color.White, false, "Tests16.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate7()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(0, 0), new Point(6, 6)));
            Test(moves, Color.White, false, "Tests16.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate8()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(3, 3), new Point(1, 1)));
            Test(moves, Color.White, true, "Tests16.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate9()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(2, 2), new Point(7, 7)));
            Test(moves, Color.White, false, "Tests17.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate10()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(4, 4), new Point(1, 1)));
            Test(moves, Color.White, false, "Tests17.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate11()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(4, 4), new Point(0, 0)));
            Test(moves, Color.White, false, "Tests17.txt");
        }
        [TestMethod]
        public void QueenAgainKillTeammate12()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(4, 4), new Point(3, 3)));
            Test(moves, Color.White, true, "Tests17.txt");
        }
        [TestMethod]
        public void QueenMustBePrettyGirl() // пешка должна рубить, становиться квиной, и оставаться на месте.
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(2, 2), new Point(0, 0)));
            Test(moves, Color.White, true, "Tests18.txt");
        }
        [TestMethod]
        public void TryAttackBeaten() // типа срубил ту парашку, он не должен этого делать.
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(2, 2), new Point(0, 0)));
            moves.Add(new Move(new Point(0, 0), new Point(7, 7)));
            Test(moves, Color.White, false, "Tests18.txt");
        }
        [TestMethod]
        public void TryAttackBeaten2() 
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(5, 5), new Point(7, 7)));
            moves.Add(new Move(new Point(7, 7), new Point(1, 1)));
            Test(moves, Color.Black, false, "Tests19.txt");
        }
        [TestMethod]
        public void TryAttackBeaten3()
        {
            var moves = new List<Move>();
            moves.Add(new Move(new Point(5, 2), new Point(7, 0)));
            moves.Add(new Move(new Point(7, 0), new Point(0, 7)));
            Test(moves, Color.White, false, "Tests20.txt");
        }
    }
}
