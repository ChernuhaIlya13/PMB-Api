namespace PMB.Abb.Models.Models
{
    public enum AbbBetType
    {
        Win,                                    //Победа 1-й команды
        Fora,                                   //	Фора1(0.0)/Без ничьи
        EuroGandicap ,                           //Еврогандикап1(%s)
        WhoWillScore,                           //обе забьёт,только одна,одна не забьёт
        Total,                                  //Тотал больше(%s)
        EvenOdd,                                //чёт/нечёт
        WinYellowCards,                         //1-Жёлтые карточки,x-Жёлтые карточки,
        ForaYellowCards,                        //Фора1(%s) - Желтые Карточки,
        TotalYellowCards,                       //Фора1(%s) - Желтые Карточки,
        EvenOddYellowCards,                     //Чет - Желтые Карточки
        WinCorners,                             //1-угловые,1X - Угловые,X - Угловые
        ForaCorners,                            //Фора1(%s) - Угловые
        TotalCorners,                           //Тотал больше(%s) - Угловые
        EvenOddCorners,                         //Нечет - Угловые
        RemovalWill,                            //Удаление будет
        Score,                                  //Счёт
        PlayerWillScore,                        //Забьёт ли игрок вышедший на замену
        WinAndTotal,                            //Победа 1 и Тотал меньше (2.5) - да
        Draw,                                   //Ничья в обоих таймах - да
        Goal,                                   //Гол в обоих таймах - нет
        Smooth,                                 //Ровно (%s) для 2-й команды - нет
        Player,                                 //Игрок1 - 1й Эйс,Игрок1 - 1й Брейк
        Replacement,                            //1я Замена - 1тайм,1я Замена - перерыв
        WinAndShotsOnTarget,                    //1 - Удары в створ,X2 - Удары в створ
        ForaAndShotsOnTarget,                   //Фора2(%s) - Удары в створ
        TotalAndShotsOnTarget,                  //Тотал больше(%s) - Удары в створ
        EvenOddAndShotsOnTarget,                //Нечет - Удары в створ,Чет - Удары в створ
        WinAndFouls,                            //12 - Фолы,X2 - Фолы
        ForaAndFouls,                           //Фора2(%s) - Фолы
        TotalAndFouls,                          //Тотал больше(%s) для 1-й команды - Фолы
        EvenOddAndFouls,                        //	Чет - Фолы
        WinAndOffsides,                         //2 - Офсайды,12 - Офсайды
        ForaAndOffsides,                        //Фора1(%s) - Офсайды
        TotalAndOffsides,                       //Тотал больше(%s) - Офсайды
        EvenOddAndOffsides,                     //Чет - Офсайды,Нечет - Офсайды
        WinAndWhoWillScore,                     //Обе забьют и Победа 2-ой команды - нет
        ExtraTime,                              //Добавочное время
        TotalAndExtraTime,                      //Тотал меньше(%s) - Добавочное время
        TotalAndReplacements,                   //Тотал больше(%s) для 1-й команды - Замены
        WinAndBallControl,                      //1X - Владение мячом
        ForaAndBallControl,                     //Фора2(%s) - Владение мячом
        TotalAndBallControl,                    //	Тотал меньше(%s) для 2-й команды - Владение мячом
        CommandAndCorners,                      //Команда 2 - 1-й Угловой
        CommandAndYellowCards,                  //Команда 1 - 1-я Желтая Карточка
        CommandAndOffside,                      //Команда 2 - Последний Офсайд
        Overtime,                               //Овертайм - нет
        ScoringDraw,                            //Результативная ничья - да
        RaceUp,                                 //Гонка до 10 - никто,Гонка до 10 - Команда 2
        CommandAndGoal,                         //Команда 1 - Следующий гол (без ничьи)
        Place,                                  //1-3место - да
        TotalAndHits,                           //Тм(%s) для 1-й команды - Хиты
        TotalAndFails,                          //Тм(%s) - Ошибки
        TotalHitsFailsJogging,                  //Тм(%s) - Хиты+Ошибки+Пробежки
        ForaAndKills,                           //Фора1(%s) - Килы
        TotalAndKills,                          //Тотал меньше(%s) - Килы
        WinAndBlood,                            //П1 - 1я кровь
        WinAndTower,                            //П1 - 1я башня
        WinAndDragon,                           //П1 - 1й дракон
        WinAndBaron,                            //П1 - 1й барон
        WinAndInhibitor,                        //	П1 - 1й ингибитор
        WinAndRoshan,                           //	П1 - 1й рошан
        WinWithPistols,                         //выиграет на пистолетах
        TotalAndDuration,                       //	Тм(%s) - Продолжительность
        TotalAndBaron,                          //Тм(%s) - Бароны
        TotalAndInhibitor,                      //Тм(%s) - Ингибиторы
        TotalAndTowers,                         //Тб(%s) - Башни
        TotalAndDragons,                        //Тб(%s) - Драконы
        TotalAndRoshans,                        //Тб(%s) - Рошаны
        TotalAndSets,                           //Тб(%s) для 1-й команды - Сеты
        WinAndTouchdown,                        //П1 - Самый длинный тачдаун,Команда 2 выиграет оба раунда на пистолетах - Да
        Touchdown,                              //Тачдаун - Да,Touchdown - Нет
        Safety,                                 //Сейфти - Нет
        Scores,                                 //Обе команды 10 очков - Нет,Обе команды 50 очков - Да
        EffectiveQuarter,                       //	Наиболее результативная четверть - 4-я
        TotalAndFieldGoal,                      //Тб(%s) - Филд голы
        TotalAndTouchdown,                      //Тб(%s) - Тачдауны
        ForaAndCards,                           //Тм(%s) - Карты
        TotalAndCards,                          //Тб(%s) для 1-й команды - Карты
        Cards,                                  //Карты (%s)
        KillTheDragon,                          //Обе ком. убьют барона - Нет
        WinAndBarak,                            //П1 - 1й барак
        WinAndKill,                             //П2 - 1й Дв.килл
        TotalAndBarak,                          //Тб(%s) - Бараки
        ForaAndBaron,                           //Ф2(%s) - Бароны
        ForaAndDragon,                          //Ф2(%s) - Драконы
        ForaAndTowers,                          //	Ф1(%s) - Башни
        ForaAndScores,                          //2 - 3х-очковые
        WinAndRebounds,                         //X - Подборы
        WinAndTransmission,                     //1 - Передачи
        ForaAndRebounds,                        //Ф2(%s) - Подборы
        ForaAndTransmission,                    //	Ф1(%s) - Передачи
        TotalAndScores,                         //Тб(%s) - 3х-очковые
        TotalAndRebounds,                       //Тм(%s) - Подборы
        TotalAndTransmission,                   //Тб(%s) - Передачи
        WinAndSeconds,                          //П1 - 180s
        ForaAndSeconds,                         //Ф1(%s) - 180s
        TotalAndSeconds,                        //Тб(%s) - 180s
        None
    }
}