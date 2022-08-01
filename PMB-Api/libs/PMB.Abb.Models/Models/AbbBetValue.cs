﻿using System.Collections.Generic;

namespace PMB.Abb.Models.Models
{
	public static class AbbBetValue
	{
		public static Dictionary<int, string> BetValues = new Dictionary<int, string>()
		{
			{1, "Победа 1-й команды"},
			{2, "Победа 2-й команды"},
			{3, "Фора1(0.0)/Без ничьи"},
			{4, "Фора2(0.0)/Без ничьи"},
			{5, "Еврогандикап1(%s)"},
			{6, "ЕврогандикапX(%s)"},
			{7, "Еврогандикап2(%s)"},
			{8, "Обе забьют"},
			{9, "Одна не забьет"},
			{10, "Только одна забьет"},
			{11, "1"},
			{12, "X"},
			{13, "2"},
			{14, "1X"},
			{15, "X2"},
			{16, "12"},
			{17, "Фора1(%s)"},
			{18, "Фора2(%s)"},
			{19, "Тотал больше(%s)"},
			{20, "Тотал меньше(%s)"},
			{21, "Тотал больше(%s) для 1-й команды"},
			{22, "Тотал меньше(%s) для 1-й команды"},
			{23, "Тотал больше(%s) для 2-й команды"},
			{24, "Тотал меньше(%s) для 2-й команды"},
			{25, "Нечет"},
			{26, "Чет"},
			{27, "1 - Желтые Карточки"},
			{28, "X - Желтые Карточки"},
			{29, "2 - Желтые Карточки"},
			{30, "1X - Желтые Карточки"},
			{31, "12 - Желтые Карточки"},
			{32, "X2 - Желтые Карточки"},
			{33, "Фора1(%s) - Желтые Карточки"},
			{34, "Фора2(%s) - Желтые Карточки"},
			{35, "Тотал больше(%s) - Желтые Карточки"},
			{36, "Тотал меньше(%s) - Желтые Карточки"},
			{37, "Тотал меньше(%s) для 1-й команды - Желтые Карточки"},
			{38, "Тотал больше(%s) для 1-й команды - Желтые Карточки"},
			{39, "Тотал меньше(%s) для 2-й команды - Желтые Карточки"},
			{40, "Тотал больше(%s) для 2-й команды - Желтые Карточки"},
			{41, "Чет - Желтые Карточки"},
			{42, "Нечет - Желтые Карточки"},
			{43, "1 - Угловые"},
			{44, "X - Угловые"},
			{45, "2 - Угловые"},
			{46, "1X - Угловые"},
			{47, "12 - Угловые"},
			{48, "X2 - Угловые"},
			{49, "Фора1(%s) - Угловые"},
			{50, "Фора2(%s) - Угловые"},
			{51, "Тотал больше(%s) - Угловые"},
			{52, "Тотал меньше(%s) - Угловые"},
			{53, "Тотал меньше(%s) для 1-й команды - Угловые"},
			{54, "Тотал больше(%s) для 1-й команды - Угловые"},
			{55, "Тотал меньше(%s) для 2-й команды - Угловые"},
			{56, "Тотал больше(%s) для 2-й команды - Угловые"},
			{57, "Нечет - Угловые"},
			{58, "Чет - Угловые"},
			{63, "Удаление будет"},
			{64, "Удаления не будет"},
			{65, "Пенальти будет"},
			{66, "Пенальти не будет"},
			{67, "Счет (%s)"},
			{68, "Тотал больше(%s) - Тай-Брейк"},
			{69, "Тотал меньше(%s) - Тай-Брейк"},
			{70, "Счет (%s) - нет"},
			{71, "Вышедший на замену - да"},
			{72, "Вышедший на замену - нет"},
			{73, "Победа 1-й команды ровно в 1 гол - да"},
			{74, "Победа 1-й команды ровно в 1 гол - нет"},
			{75, "Победа 2-й команды ровно в 1 гол - да"},
			{76, "Победа 2-й команды ровно в 1 гол - нет"},
			{77, "Победа 1-й команды ровно в 2 гола - да"},
			{78, "Победа 1-й команды ровно в 2 гола - нет"},
			{79, "Победа 1-й команды ровно в 3 гола - да"},
			{80, "Победа 1-й команды ровно в 3 гола - нет"},
			{81, "Победа 2-й команды ровно в 2 гола - да"},
			{82, "Победа 2-й команды ровно в 2 гола - нет"},
			{83, "Победа 2-й команды ровно в 3 гола - да"},
			{84, "Победа 2-й команды ровно в 3 гола - нет"},
			{85, "Сухая победа 1-й команды - да"},
			{86, "Сухая победа 1-й команды - нет"},
			{87, "Сухая победа 2-й команды - да"},
			{88, "Сухая победа 2-й команды - нет"},
			{89, "Победа 1-й команды хотя бы в одном тайме - да"},
			{90, "Победа 1-й команды хотя бы в одном тайме - нет"},
			{91, "Победа 2-й команды хотя бы в одном тайме - да"},
			{92, "Победа 2-й команды хотя бы в одном тайме - нет"},
			{93, "Ничья хотя бы в одном тайме - да"},
			{94, "Ничья хотя бы в одном тайме - нет"},
			{95, "Победа 1-й команды в обоих таймах - да"},
			{96, "Победа 1-й команды в обоих таймах - нет"},
			{97, "Победа 2-й команды в обоих таймах - да"},
			{98, "Победа 2-й команды в обоих таймах - нет"},
			{99, "Победа 1 и Тотал больше (2.5) - да"},
			{100, "Победа 1 и Тотал больше (2.5) - нет"},
			{101, "Победа 1 и Тотал меньше (2.5) - да"},
			{102, "Победа 1 и Тотал меньше (2.5) - нет"},
			{103, "Победа 2 и Тотал больше (2.5) - да"},
			{104, "Победа 2 и Тотал больше (2.5) - нет"},
			{105, "Победа 2 и Тотал меньше (2.5) - да"},
			{106, "Победа 2 и Тотал меньше (2.5) - нет"},
			{107, "Ничья в обоих таймах - да"},
			{108, "Ничья в обоих таймах - нет"},
			{109, "Ничья и Тотал больше (2.5) - да"},
			{110, "Ничья и Тотал больше (2.5) - нет"},
			{111, "Ничья и Тотал меньше (2.5) - да"},
			{112, "Ничья и Тотал меньше (2.5) - нет"},
			{113, "Гол в обоих таймах - да"},
			{114, "Гол в обоих таймах - нет"},
			{115, "Команда 1 - гол в обоих таймах - да"},
			{116, "Команда 1 - гол в обоих таймах - нет"},
			{117, "Команда 2 - гол в обоих таймах - да"},
			{118, "Команда 2 - гол в обоих таймах - нет"},
			{119, "Дубль - да"},
			{120, "Дубль - нет"},
			{121, "Хет-трик - да"},
			{122, "Хет-трик - нет"},
			{123, "Автогол - да"},
			{124, "Автогол - нет"},
			{125, "Оба тайма тотал больше 1.5 - да"},
			{126, "Оба тайма тотал больше 1.5 - нет"},
			{127, "Оба тайма тотал меньше 1.5 - да"},
			{128, "Оба тайма тотал меньше 1.5 - нет"},
			{129, "Счет (%s) - Сеты"},
			{130, "Счет (%s) - Сеты - нет"},
			{131, "Фора1(%s) - Сеты"},
			{132, "Фора2(%s) - Сеты"},
			{133, "Тотал больше(%s) - Сеты"},
			{134, "Тотал меньше(%s) - Сеты"},
			{135, "Победа1/Победа1"},
			{136, "Победа1/Победа1 - нет"},
			{137, "Победа1/Ничья"},
			{138, "Победа1/Ничья - нет"},
			{139, "Победа1/Победа2"},
			{140, "Победа1/Победа2 - нет"},
			{141, "Ничья/Победа1"},
			{142, "Ничья/Победа1 - нет"},
			{143, "Ничья/Ничья"},
			{144, "Ничья/Ничья - нет"},
			{145, "Ничья/Победа2"},
			{146, "Ничья/Победа2 - нет"},
			{147, "Победа2/Победа1"},
			{148, "Победа2/Победа1 - нет"},
			{149, "Победа2/Ничья"},
			{150, "Победа2/Ничья - нет"},
			{151, "Победа2/Победа2"},
			{152, "Победа2/Победа2 - нет"},
			{153, "Ровно (%s)"},
			{154, "Ровно (%s) - нет"},
			{155, "Ровно (%s) для 1-й команды"},
			{156, "Ровно (%s) для 1-й команды - нет"},
			{157, "Ровно (%s) для 2-й команды"},
			{158, "Ровно (%s) для 2-й команды - нет"},
			{159, "1 тайм больше голов"},
			{160, "Равное кол-во голов"},
			{161, "2 тайм больше голов"},
			{162, "1 тайм больше голов (без ничьи)"},
			{163, "2 тайм больше голов (без ничьи)"},
			{164, "Команда 1 - 1-й гол"},
			{165, "Без голов"},
			{166, "Команда 2 - 1-й гол"},
			{167, "1 гол - хозяева (без ничьи)"},
			{168, "1 гол - гости (без ничьи)"},
			{169, "Команда 1 - Последний гол"},
			{170, "Без голов"},
			{171, "Команда 2 - Последний гол"},
			{172, "Команда 1 - Последний гол (без ничьи)"},
			{173, "Команда 2 - Последний гол (без ничьи)"},
			{174, "Тотал больше(%s) - Эйсы"},
			{175, "Тотал меньше(%s) - Эйсы"},
			{176, "Тотал больше(%s) для 1-й команды - Эйсы"},
			{177, "Тотал меньше(%s) для 1-й команды - Эйсы"},
			{178, "Тотал больше(%s) для 2-й команды - Эйсы"},
			{179, "Тотал меньше(%s) для 2-й команды - Эйсы"},
			{180, "Тотал больше(%s) - Двойные ошибки"},
			{181, "Тотал меньше(%s) - Двойные ошибки"},
			{182, "Тотал больше(%s) для 1-й команды - Двойные ошибки"},
			{183, "Тотал меньше(%s) для 1-й команды - Двойные ошибки"},
			{184, "Тотал больше(%s) для 2-й команды - Двойные ошибки"},
			{185, "Тотал меньше(%s) для 2-й команды - Двойные ошибки"},
			{186, "Тотал больше(%s) для 1-й команды - Первая подача"},
			{187, "Тотал меньше(%s) для 1-й команды - Первая подача"},
			{188, "Тотал больше(%s) для 2-й команды - Первая подача"},
			{189, "Тотал меньше(%s) для 2-й команды - Первая подача"},
			{190, "Фора1(%s) - Эйсы"},
			{191, "Фора2(%s) - Эйсы"},
			{192, "Фора1(%s) - Двойные ошибки"},
			{193, "Фора2(%s) - Двойные ошибки"},
			{194, "Фора1(%s) - Первая подача"},
			{195, "Фора2(%s) - Первая подача"},
			{196, "Игрок1 - 1й Эйс"},
			{197, "Игрок2 - 1й Эйс"},
			{198, "Игрок1 - 1я Двойная ошибка"},
			{199, "Игрок2 - 1я Двойная ошибка"},
			{200, "Игрок1 - 1й Брейк"},
			{201, "Игрок2 - 1й Брейк"},
			{202, "Игрок1 - 1й Брейк"},
			{203, "Без Брейка"},
			{204, "Игрок2 - 1й Брейк"},
			{205, "6-0 Сет - да"},
			{206, "6-0 Сет - нет"},
			{207, "Волевая победа - да"},
			{208, "Волевая победа - нет"},
			{209, "Ровно (%s) - Сеты"},
			{210, "Ровно (%s) - Сеты - нет"},
			{211, "Команда 1 - 1-й Угловой"},
			{212, "Без угловых"},
			{213, "Команда 2 - 1-й Угловой"},
			{214, "Команда 1 - Последний Угловой"},
			{215, "Без угловых"},
			{216, "Команда 2 - Последний Угловой"},
			{217, "Команда 1 - 1-я Желтая Карточка"},
			{218, "Без Желтых Карточек"},
			{219, "Команда 2 - 1-я Желтая Карточка"},
			{220, "Команда 1 - Последняя Желтая Карточка"},
			{221, "Без Желтых Карточек"},
			{222, "Команда 2 - Последняя Желтая Карточка"},
			{223, "Команда 1 - 1-й Офсайд"},
			{224, "Без Офсайдов"},
			{225, "Команда 2 - 1-й Офсайд"},
			{226, "Команда 1 - Последний Офсайд"},
			{227, "Без Офсайдов"},
			{228, "Команда 2 - Последний Офсайд"},
			{229, "1я Замена - 1тайм"},
			{230, "1я Замена - перерыв"},
			{231, "1я Замена - 2тайм"},
			{232, "1й Гол - 1тайм"},
			{233, "Без голов"},
			{234, "1й Гол - 2тайм"},
			{235, "Команда 1 - 1-я Замена"},
			{236, "Команда 2 - 1-я Замена"},
			{237, "Команда 1 - Последняя Замена"},
			{238, "Команда 1 - Последняя Замена"},
			{239, "1 - Удары в створ"},
			{240, "X - Удары в створ"},
			{241, "2 - Удары в створ"},
			{242, "1X - Удары в створ"},
			{243, "12 - Удары в створ"},
			{244, "X2 - Удары в створ"},
			{245, "Фора1(%s) - Удары в створ"},
			{246, "Фора2(%s) - Удары в створ"},
			{247, "Тотал больше(%s) - Удары в створ"},
			{248, "Тотал меньше(%s) - Удары в створ"},
			{249, "Тотал больше(%s) для 1-й команды - Удары в створ"},
			{250, "Тотал меньше(%s) для 1-й команды - Удары в створ"},
			{251, "Тотал больше(%s) для 2-й команды - Удары в створ"},
			{252, "Тотал меньше(%s) для 2-й команды - Удары в створ"},
			{253, "Нечет - Удары в створ"},
			{254, "Чет - Удары в створ"},
			{255, "1 - Фолы"},
			{256, "X - Фолы"},
			{257, "2 - Фолы"},
			{258, "1X - Фолы"},
			{259, "12 - Фолы"},
			{260, "X2 - Фолы"},
			{261, "Фора1(%s) - Фолы"},
			{262, "Фора2(%s) - Фолы"},
			{263, "Тотал больше(%s) - Фолы"},
			{264, "Тотал меньше(%s) - Фолы"},
			{265, "Тотал больше(%s) для 1-й команды - Фолы"},
			{266, "Тотал меньше(%s) для 1-й команды - Фолы"},
			{267, "Тотал больше(%s) для 2-й команды - Фолы"},
			{268, "Тотал меньше(%s) для 2-й команды - Фолы"},
			{269, "Нечет - Фолы"},
			{270, "Чет - Фолы"},
			{271, "1 - Офсайды"},
			{272, "X - Офсайды"},
			{273, "2 - Офсайды"},
			{274, "1X - Офсайды"},
			{275, "12 - Офсайды"},
			{276, "X2 - Офсайды"},
			{277, "Фора1(%s) - Офсайды"},
			{278, "Фора2(%s) - Офсайды"},
			{279, "Тотал больше(%s) - Офсайды"},
			{280, "Тотал меньше(%s) - Офсайды"},
			{281, "Тотал больше(%s) для 1-й команды - Офсайды"},
			{282, "Тотал меньше(%s) для 1-й команды - Офсайды"},
			{283, "Тотал больше(%s) для 2-й команды - Офсайды"},
			{284, "Тотал меньше(%s) для 2-й команды - Офсайды"},
			{285, "Нечет - Офсайды"},
			{286, "Чет - Офсайды"},
			{287, "Волевая победа 1-й команды - да"},
			{288, "Волевая победа 1-й команды - нет"},
			{289, "Волевая победа 2-й команды - да"},
			{290, "Волевая победа 2-й команды - нет"},
			{291, "Обе забьют и Победа 1-ой команды - да"},
			{292, "Обе забьют и Победа 1-ой команды - нет"},
			{293, "Обе забьют и Победа 2-ой команды - да"},
			{294, "Обе забьют и Победа 2-ой команды - нет"},
			{295, "Обе забьют и Ничья - да"},
			{296, "Обе забьют и Ничья - нет"},
			{297, "Точно (%s) - Добавочное время"},
			{298, "Точно (%s) - Добавочное время - нет"},
			{299, "Тотал больше(%s) - Добавочное время"},
			{300, "Тотал меньше(%s) - Добавочное время"},
			{301, "Без П1 - П2"},
			{302, "Без П1 - Ничья"},
			{303, "Без П1 - Не ничья"},
			{304, "Без П2 - П1"},
			{305, "Без П2 - Ничья"},
			{306, "Без П2 - Не ничья"},
			{307, "Тотал больше(%s) - Замены"},
			{308, "Тотал меньше(%s) - Замены"},
			{309, "Тотал больше(%s) для 1-й команды - Замены"},
			{310, "Тотал меньше(%s) для 1-й команды - Замены"},
			{311, "Тотал больше(%s) для 2-й команды - Замены"},
			{312, "Тотал меньше(%s) для 2-й команды - Замены"},
			{313, "1 - Владение мячом"},
			{314, "X - Владение мячом"},
			{315, "2 - Владение мячом"},
			{316, "1X - Владение мячом"},
			{317, "12 - Владение мячом"},
			{318, "X2 - Владение мячом"},
			{319, "Фора1(%s) - Владение мячом"},
			{320, "Фора2(%s) - Владение мячом"},
			{321, "Тотал больше(%s) для 1-й команды - Владение мячом"},
			{322, "Тотал меньше(%s) для 1-й команды - Владение мячом"},
			{323, "Тотал больше(%s) для 2-й команды - Владение мячом"},
			{324, "Тотал меньше(%s) для 2-й команды - Владение мячом"},
			{325, "Команда 1 - 1-й Угловой"},
			{326, "Команда 2 - 1-й Угловой"},
			{327, "Команда 1 - Последний Угловой"},
			{328, "Команда 2 - Последний Угловой"},
			{329, "Команда 1 - 1-я Желтая Карточка"},
			{330, "Команда 2 - 1-я Желтая Карточка"},
			{331, "Команда 1 - Последняя Желтая Карточка"},
			{332, "Команда 2 - Последняя Желтая Карточка"},
			{333, "Команда 1 - 1-й Офсайд"},
			{334, "Команда 1 - 1-й Офсайд"},
			{335, "Команда 2 - Последний Офсайд"},
			{336, "Команда 2 - Последний Офсайд"},
			{337, "Без П1 - Не П2"},
			{338, "Без П2 - Не П1"},
			{339, "1X и Тотал больше (2.5) - да"},
			{340, "1X и Тотал больше (2.5) - нет"},
			{341, "1X и Тотал меньше (2.5) - да"},
			{342, "1X и Тотал меньше (2.5) - нет"},
			{343, "X2 и Тотал больше (2.5) - да"},
			{344, "X2 и Тотал больше (2.5) - нет"},
			{345, "X2 и Тотал меньше (2.5) - да"},
			{346, "X2 и Тотал меньше (2.5) - нет"},
			{348, "Овертайм - да"},
			{351, "Овертайм - нет"},
			{354, "Результативная ничья - да"},
			{357, "Результативная ничья - нет"},
			{360, "Гонка до 2 - Команда 1"},
			{363, "Гонка до 2 - Команда 2"},
			{366, "Гонка до 2 - Команда 1"},
			{369, "Гонка до 2 - никто"},
			{372, "Гонка до 2 - Команда 2"},
			{375, "Гонка до 3 - Команда 1"},
			{378, "Гонка до 3 - Команда 2"},
			{381, "Гонка до 3 - Команда 1"},
			{384, "Гонка до 3 - никто"},
			{387, "Гонка до 3 - Команда 2"},
			{390, "Гонка до 4 - Команда 1"},
			{393, "Гонка до 4 - Команда 2"},
			{396, "Гонка до 4 - Команда 1"},
			{399, "Гонка до 4 - никто"},
			{402, "Гонка до 4 - Команда 2"},
			{405, "Гонка до 5 - Команда 1"},
			{408, "Гонка до 5 - Команда 2"},
			{411, "Гонка до 5 - Команда 1"},
			{414, "Гонка до 5 - никто"},
			{417, "Гонка до 5 - Команда 2"},
			{420, "Гонка до 10 - Команда 1"},
			{423, "Гонка до 10 - Команда 2"},
			{426, "Гонка до 10 - Команда 1"},
			{429, "Гонка до 10 - никто"},
			{432, "Гонка до 10 - Команда 2"},
			{435, "Гонка до 15 - Команда 1"},
			{438, "Гонка до 15 - Команда 2"},
			{441, "Гонка до 15 - Команда 1"},
			{444, "Гонка до 15 - никто"},
			{447, "Гонка до 15 - Команда 2"},
			{450, "Гонка до 20 - Команда 1"},
			{453, "Гонка до 20 - Команда 2"},
			{456, "Гонка до 20 - Команда 1"},
			{459, "Гонка до 20 - никто"},
			{462, "Гонка до 20 - Команда 2"},
			{467, "Команда 1 - Следующий гол (без ничьи)"},
			{470, "Команда 2 - Следующий гол (без ничьи)"},
			{473, "Команда 1 - Следующий гол"},
			{476, "След.гола не будет"},
			{479, "Команда 2 - Следующий гол"},
			{481, "1место - да"},
			{484, "1место - нет"},
			{487, "1-3место - да"},
			{490, "1-3место - нет"},
			{493, "П1"},
			{496, "П2"},
			{499, "Тб(%s) - Хиты"},
			{502, "Тм(%s) - Хиты"},
			{505, "Тб(%s) для 1-й команды - Хиты"},
			{508, "Тм(%s) для 1-й команды - Хиты"},
			{511, "Тб(%s) для 2-й команды - Хиты"},
			{514, "Тм(%s) для 2-й команды - Хиты"},
			{517, "Тб(%s) - Ошибки"},
			{520, "Тм(%s) - Ошибки"},
			{523, "Тб(%s) - Хиты+Ошибки+Пробежки"},
			{526, "Тм(%s) - Хиты+Ошибки+Пробежки"},
			{529, "Ф1(%s) - Хиты"},
			{532, "Ф2(%s) - Хиты"},
			{535, "1 - Хиты"},
			{538, "X - Хиты"},
			{541, "2 - Хиты"},
			{546, "П1 - Килы"},
			{549, "П2 - Килы"},
			{552, "Фора1(%s) - Килы"},
			{555, "Фора2(%s) - Килы"},
			{558, "Тотал больше(%s) - Килы"},
			{561, "Тотал меньше(%s) - Килы"},
			{564, "Тотал больше(%s) для 1-й команды - Килы"},
			{567, "Тотал меньше(%s) для 1-й команды - Килы"},
			{570, "Тотал больше(%s) для 2-й команды - Килы"},
			{573, "Тотал меньше(%s) для 2-й команды - Килы"},
			{574, "П1 - 1я кровь"},
			{575, "П2 - 1я кровь"},
			{576, "П1 - 1я башня"},
			{577, "П2 - 1я башня"},
			{578, "П1 - 1й дракон"},
			{579, "П2 - 1й дракон"},
			{580, "П1 - 1й барон"},
			{581, "П2 - 1й барон"},
			{582, "П1 - 1й ингибитор"},
			{583, "П2 - 1й ингибитор"},
			{584, "П1 - 1й рошан"},
			{585, "П2 - 1й рошан"},
			{586, "Выиграет на пистолетах - Да"},
			{587, "Выиграет на пистолетах - Нет"},
			{588, "Тб(%s) - Продолжительность"},
			{589, "Тм(%s) - Продолжительность"},
			{590, "Тб(%s) - Бароны"},
			{591, "Тм(%s) - Бароны"},
			{592, "Тб(%s) - Ингибиторы"},
			{593, "Тм(%s) - Ингибиторы"},
			{594, "Тб(%s) - Башни"},
			{595, "Тм(%s) - Башни"},
			{596, "Тб(%s) - Драконы"},
			{597, "Тм(%s) - Драконы"},
			{598, "Тб(%s) - Рошаны"},
			{599, "Тм(%s) - Рошаны"},
			{600, "Тб(%s) для 1-й команды - Сеты"},
			{601, "Тм(%s) для 1-й команды - Сеты"},
			{602, "Тб(%s) для 2-й команды - Сеты"},
			{603, "Тм(%s) для 2-й команды - Сеты"},
			{604, "П1 - Самый длинный тачдаун"},
			{605, "П2 - Самый длинный тачдаун"},
			{606, "П1 - Самый длинный филд гол"},
			{607, "П2 - Самый длинный филд гол"},
			{608, "Тачдаун - Да"},
			{609, "Touchdown - Нет"},
			{610, "Сейфти - Да"},
			{611, "Сейфти - Нет"},
			{612, "Первые очки ТД - Да"},
			{613, "Первые очки ТД - Нет"},
			{614, "Обе команды 10 очков - Да"},
			{615, "Обе команды 10 очков - Нет"},
			{616, "Обе команды 15 очков - Да"},
			{617, "Обе команды 15 очков - Нет"},
			{618, "Обе команды 20 очков - Да"},
			{619, "Обе команды 20 очков - Нет"},
			{620, "Обе команды 25 очков - Да"},
			{621, "Обе команды 25 очков - Нет"},
			{622, "Обе команды 30 очков - Да"},
			{623, "Обе команды 30 очков - Нет"},
			{624, "Обе команды 35 очков - Да"},
			{625, "Обе команды 35 очков - Нет"},
			{626, "Обе команды 40 очков - Да"},
			{627, "Обе команды 40 очков - Нет"},
			{628, "Обе команды 45 очков - Да"},
			{629, "Обе команды 45 очков - Нет"},
			{630, "Обе команды 50 очков - Да"},
			{631, "Обе команды 50 очков - Нет"},
			{632, "Наиболее результативная четверть - 1-я"},
			{633, "Наиболее результативная четверть - 2-я"},
			{634, "Наиболее результативная четверть - 3-я"},
			{635, "Наиболее результативная четверть - 4-я"},
			{636, "Наиболее результативная четверть - Ровно"},
			{637, "Тб(%s) - Филд голы"},
			{638, "Тм(%s) - Филд голы"},
			{639, "Тб(%s) - Тачдауны"},
			{640, "Тм(%s) - Тачдауны"},
			{641, "Тб(%s) - Самый длинный тачдаун, метры"},
			{642, "Тм(%s) - Самый длинный тачдаун, метры"},
			{643, "Тб(%s) - Самый длинный филд гол, метры"},
			{644, "Тм(%s) - Самый длинный филд гол, метры"},
			{645, "Тб(%s) для 1-й команды - Филд голы"},
			{646, "Тм(%s) для 1-й команды - Филд голы"},
			{647, "Тб(%s) для 2-й команды - Филд голы"},
			{648, "Тм(%s) для 2-й команды - Филд голы"},
			{649, "Тб(%s) для 1-й команды - Тачдауны"},
			{650, "Тм(%s) для 1-й команды - Тачдауны"},
			{651, "Тб(%s) для 2-й команды - Тачдауны"},
			{652, "Тм(%s) для 2-й команды - Тачдауны"},
			{653, "Ф1(%s) - Карты"},
			{654, "Ф2(%s) - Карты"},
			{655, "Тб(%s) - Карты"},
			{656, "Тм(%s) - Карты"},
			{657, "Тб(%s) для 1-й команды - Карты"},
			{658, "Тм(%s) для 1-й команды - Карты"},
			{659, "Тб(%s) для 2-й команды - Карты"},
			{660, "Тм(%s) для 2-й команды - Карты"},
			{661, "Карты (%s)"},
			{662, "Карты (%s) - нет"},
			{663, "Ровно (%s) - Карты"},
			{664, "Ровно (%s) - Карты - нет"},
			{665, "Выиграет оба раунда на пистолетах - Да"},
			{666, "Выиграет оба раунда на пистолетах - Нет"},
			{667, "Команда 1 выиграет оба раунда на пистолетах - Да"},
			{668, "Команда 1 выиграет оба раунда на пистолетах - Нет"},
			{669, "Команда 2 выиграет оба раунда на пистолетах - Да"},
			{670, "Команда 2 выиграет оба раунда на пистолетах - Нет"},
			{671, "Команда 1 выиграет один сет - Да"},
			{672, "Команда 1 выиграет один сет - Нет"},
			{673, "Команда 2 выиграет один сет - Да"},
			{674, "Команда 2 выиграет один сет - Нет"},
			{675, "Команда 1 выиграет одну карту - Да"},
			{676, "Команда 1 выиграет одну карту - Нет"},
			{677, "Команда 2 выиграет одну карту - Да"},
			{678, "Команда 2 выиграет одну карту - Нет"},
			{679, "Обе ком. убьют дракона - Да"},
			{680, "Обе ком. убьют дракона - Нет"},
			{681, "Обе ком. убьют барона - Да"},
			{682, "Обе ком. убьют барона - Нет"},
			{683, "П1 - 1й барак"},
			{684, "П2 - 11 барак"},
			{685, "П1 - 1й Дв.килл"},
			{686, "П2 - 1й Дв.килл"},
			{687, "Тб(%s) - Бараки"},
			{688, "Тм(%s) - Бараки"},
			{689, "Тб(%s) - Дв.киллы"},
			{690, "Тм(%s) - Дв.киллы"},
			{691, "Ф1(%s) - Бароны"},
			{692, "Ф2(%s) - Бароны"},
			{693, "Ф1(%s) - Драконы"},
			{694, "Ф2(%s) - Драконы"},
			{695, "Ф1(%s) - Башни"},
			{696, "Ф2(%s) - Башни"},
			{697, "1 - 3х-очковые"},
			{698, "X - 3х-очковые"},
			{699, "2 - 3х-очковые"},
			{700, "1 - Подборы"},
			{701, "X - Подборы"},
			{702, "2 - Подборы"},
			{703, "1 - Передачи"},
			{704, "X - Передачи"},
			{705, "2 - Передачи"},
			{706, "1X - 3х-очковые"},
			{707, "X2 - 3х-очковые"},
			{708, "12 - 3х-очковые"},
			{709, "1X - Подборы"},
			{710, "X2 - Подборы"},
			{711, "12 - Подборы"},
			{712, "1X - Передачи"},
			{713, "X2 - Передачи"},
			{714, "12 - Передачи"},
			{715, "Ф1(%s) - 3х-очковые"},
			{716, "Ф2(%s) - 3х-очковые"},
			{717, "Ф1(%s) - Подборы"},
			{718, "Ф2(%s) - Подборы"},
			{719, "Ф1(%s) - Передачи"},
			{720, "Ф2(%s) - Передачи"},
			{721, "Тб(%s) - 3х-очковые"},
			{722, "Тм(%s) - 3х-очковые"},
			{723, "Тб(%s) - Подборы"},
			{724, "Тм(%s) - Подборы"},
			{725, "Тб(%s) - Передачи"},
			{726, "Тм(%s) - Передачи"},
			{727, "Тб(%s) для 1-й команды - 3х-очковые"},
			{728, "Тм(%s) для 1-й команды - 3х-очковые"},
			{729, "Тб(%s) для 1-й команды - Подборы"},
			{730, "Тм(%s) для 1-й команды - Подборы"},
			{731, "Тб(%s) для 1-й команды - Передачи"},
			{732, "Тм(%s) для 1-й команды - Передачи"},
			{733, "Тб(%s) для 2-й команды - 3х-очковые"},
			{734, "Тм(%s) для 2-й команды - 3х-очковые"},
			{735, "Тб(%s) для 2-й команды - Подборы"},
			{736, "Тм(%s) для 2-й команды - Подборы"},
			{737, "Тб(%s) для 2-й команды - Передачи"},
			{738, "Тм(%s) для 2-й команды - Передачи"},
			{739, "П1 - 180s"},
			{740, "П2 - 180s"},
			{741, "Ф1(%s) - 180s"},
			{742, "Ф1(%s) - 180s"},
			{743, "Тб(%s) - 180s"},
			{744, "Тм(%s) - 180s"},
		};
	}
}