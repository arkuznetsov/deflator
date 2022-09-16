# Oscript Deflate component

[![GitHub release](https://img.shields.io/github/release/ArKuznetsov/deflator.svg?style=flat-square)](https://github.com/ArKuznetsov/deflator/releases)
[![GitHub license](https://img.shields.io/github/license/ArKuznetsov/deflator.svg?style=flat-square)](https://github.com/ArKuznetsov/deflator/blob/master/LICENSE)
[![GitHub Releases](https://img.shields.io/github/downloads/ArKuznetsov/deflator/latest/total?style=flat-square)](https://github.com/ArKuznetsov/deflator/releases)
[![GitHub All Releases](https://img.shields.io/github/downloads/ArKuznetsov/deflator/total?style=flat-square)](https://github.com/ArKuznetsov/deflator/releases)

[![Build Status](https://img.shields.io/github/workflow/status/ArKuznetsov/deflator/%D0%9A%D0%BE%D0%BD%D1%82%D1%80%D0%BE%D0%BB%D1%8C%20%D0%BA%D0%B0%D1%87%D0%B5%D1%81%D1%82%D0%B2%D0%B0)](https://github.com/arkuznetsov/deflator/actions/)
[![Quality Gate](https://open.checkbsl.org/api/project_badges/measure?project=deflator&metric=alert_status)](https://open.checkbsl.org/dashboard/index/deflator)
[![Coverage](https://open.checkbsl.org/api/project_badges/measure?project=deflator&metric=coverage)](https://open.checkbsl.org/dashboard/index/deflator)
[![Tech debt](https://open.checkbsl.org/api/project_badges/measure?project=deflator&metric=sqale_index)](https://open.checkbsl.org/dashboard/index/deflator)

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
