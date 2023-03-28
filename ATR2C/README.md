# ATR2C

The ATR2C - AnyTone Repeater 2 CSV - parses the ÖVSV repeater list (`.xlsx` converted to `.csv`) for **70cm** and **2m** voice repeaters. It then creates zones and channels based on the data. It also adds default data if specified and an AnalogAddressBook containing the EchoLink DTMF codes for the respective repeater. 

## Usage

The program needs 2 files to be present in its directory:

1. repeater.csv - a CSV file with information about repeaters
2. talkgroups.csv - a CSV file containing talkgroups

The program creates
1. a *zone* for each DMR repeater containing
   1. n channels with the Tx and Rx frequencies of the channel with a predefined TG (n = number of talkgroups defined for the network of the repeater, defined in `/input/talkgroups.csv`)
2. a *zone* for every region (OE1 - OE9) containing
   1. all analog repeaters of the region.

### Input Files

Column mappings can be changed in `Settings.cs`.

#### `repeater.csv` file

Needs the following columns:

1. band
2. Tx
3. Rx
4. Callsign
5. location
6. IPSC2 (true/false)
7. Brandmeister (true/false)
8. colorcode
9. DMR (true/false)
10. FM (true/false)
11. ctcsstx
12. ctcssrx

Must have a header row.

#### `talkgroups.csv` file

needs the following columns

1. ID (row in CPS)
2. Radio ID (wrongfully labeled so, should be DMR ID)
3. Name (Name of the talkgroup)
4. Call Type (Typically Group Call)
5. Call Alert (Typically None)
6. Create Channel (TRUE for the most common talk groups)
7. Add To List (TRUE if the talkgroup should be accessible via the menu on the HT)
8. Network (Is it a Brandmeister (BM) or IPSC2 (I2) talk group?)
9. TimeSlot (on which timeslot should the talkgroup be sent?)

must have a header row.

### Defaults Files

**Caution:** If there are talkgroups mentioned in `/defaults/Channel.csv`, then those talkgroups must be present in `/input/talkgroups.csv`.

## Remarks

1. bandwidth is always set to 12.5K as that is the predominant analog FM raster in Europe. Also, DMR requires 12.5;

## Known Issues

1. Some Repeater have different settings on repeater.oevsv.at and dmraustria.at