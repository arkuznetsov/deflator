# Oscript Deflate component

[![GitHub release](https://img.shields.io/github/release/ArKuznetsov/oscript-deflate.svg?style=flat-square)](https://github.com/ArKuznetsov/oscript-deflate/releases)
[![GitHub license](https://img.shields.io/github/license/ArKuznetsov/oscript-deflate.svg?style=flat-square)](https://github.com/ArKuznetsov/oscript-deflate/blob/master/LICENSE)
[![GitHub Releases](https://img.shields.io/github/downloads/ArKuznetsov/oscript-deflate/latest/total?style=flat-square)](https://github.com/ArKuznetsov/oscript-deflate/releases)
[![GitHub All Releases](https://img.shields.io/github/downloads/ArKuznetsov/oscript-deflate/total?style=flat-square)](https://github.com/ArKuznetsov/oscript-deflate/releases)

[![Build Status](https://img.shields.io/github/workflow/status/ArKuznetsov/oscript-deflate/%D0%9A%D0%BE%D0%BD%D1%82%D1%80%D0%BE%D0%BB%D1%8C%20%D0%BA%D0%B0%D1%87%D0%B5%D1%81%D1%82%D0%B2%D0%B0)](https://github.com/arkuznetsov/oscript-deflate/actions/)
[![Quality Gate](https://open.checkbsl.org/api/project_badges/measure?project=oscript-deflate&metric=alert_status)](https://open.checkbsl.org/dashboard/index/oscript-deflate)
[![Coverage](https://open.checkbsl.org/api/project_badges/measure?project=oscript-deflate&metric=coverage)](https://open.checkbsl.org/dashboard/index/oscript-deflate)
[![Tech debt](https://open.checkbsl.org/api/project_badges/measure?project=oscript-deflate&metric=sqale_index)](https://open.checkbsl.org/dashboard/index/oscript-deflate)

## Компонента упаковки / распаковки по алгоритму Deflate для oscript

## Примеры использования

### Упаковка потока

```bsl
#Использовать deflator

ВходящийПоток = Новый ФайловыйПоток("d:\tmp\inputFile.txt");
УпакованныйПоток = Новый ПотокВПамяти();

Упаковщик = Новый УпаковщикDeflate();
Упаковщик.УпаковатьПоток(ВходящийПоток, УпакованныйПоток, 1);

```

### Распаковка потока

```bsl
#Использовать deflator

УпакованныйПоток = Новый ПотокВПамяти();
ИсходящийПоток = Новый ФайловыйПоток("d:\tmp\outputFile.txt");

Упаковщик = Новый УпаковщикDeflate();
Упаковщик.РаспаковатьПоток(УпакованныйПоток, ИсходящийПоток);

```

### Упаковка двоичных данных

```bsl
#Использовать deflator

ВходящиеДанные = Новый ДвоичныеДанные("d:\tmp\inputFile.txt");

Упаковщик = Новый УпаковщикDeflate();
УпакованныеДанные = Упаковщик.УпаковатьДанные(ВходящиеДанные, 1);

```

### Распаковка двоичных данных

```bsl
#Использовать deflator

УпакованныеДанные = Новый ДвоичныеДанные("d:\tmp\compressedFile.dfl");

Упаковщик = Новый УпаковщикDeflate();
ИсходящиеДанные = Упаковщик.РаспаковатьДанные(УпакованныеДанные);

```

### Упаковка файла

```bsl
#Использовать deflator
 
Упаковщик = Новый УпаковщикDeflate();
Упаковщик.УпаковатьФайл("d:\tmp\inputFile.txt", "d:\tmp\compressedFile.dfl");

```

### Распаковка файла

```bsl
#Использовать deflator

Упаковщик = Новый УпаковщикDeflate();
Упаковщик.РаспаковатьФайл("d:\tmp\compressedFile.dfl", "d:\tmp\outputFile.txt");

```
