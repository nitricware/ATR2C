# ATR2C

The ATR2C - AnyTone Repeater 2 CSV - parses the ÖVSV repeater list (`.xlsx` converted to `.csv`) for **70cm** and **2m** voice repeaters. It then creates zones and channels based on the data. It also adds default data if specified and an AnalogAddressBook containing the EchoLink DTMF codes for the respective repeater. 

The program
1. creates a *zone* for each DMR repeater containing
   1. n channels with the Tx and Rx frequencies of the channel with a predefined TG (n = number of talkgroups defined for the network of the repeater, defined in `/input/talkgroups.csv`)
2. creates a *zone* for every region (OE1 - OE9) containing all analog repeaters of the region.
3. creates an Analog Address Book containing the EchoLink DTMF Codes for every repeater in the input file
4. creates a digital and analog Scanlist for every location prefix (i.e. "OE3", "HB9") containing all analog repeaters and selected talkgroups of a the region's digital repeaters
5. merges predefined zones, channels, analog address books and scanlists with the generated lists

## Usage

### Preparation

1. `repeater.csv`
	- Either download the lates Excel export from here: https://www.oevsv.at/funkbetrieb/amateurfunkfrequenzen/ukw-referat/maps/ (direct link to the Excel export: https://repeater.oevsv.at/static/lists_raw_data.xlsx) and convert the `.xlsx` file to `repeater.csv` (delimiter: `,`)
	- or create a custom `repeater.csv`  file (see section "Input Files")
2. Prepare a `talk groups.csv` file (see section "Input Files")
3. Put both files in a folder `input` relative to the executable 

### Input Files

The application is meant to work with the column scheme of the `.xlsx`/`.csv` from repeater.oevsv.at; however, column mappings can be changed in `Settings.cs`.

Both input files need a header row. The column order is relevant to the program, the column naming is irrelevant to the program.

In the following the required columns for each file are listed.

#### `repeater.csv` file

1. band
2. Tx
3. Rx
4. Callsign
5. location
6. IPSC2 (1 if yes, empty if no)
7. Brandmeister (1 if yes, empty if no)
8. colorcode
9. DMR (1 if yes, empty if no)
10. FM (1 if yes, empty if no)
11. ctcsstx
12. ctcssrx

#### `talkgroups.csv` file

1. ID
2. Radio ID
	- wrongfully labeled so, should be DMR ID
	- Radio ID must be defined in CPS beforehand and exist at time of import
3. Name
	- Name of the talkgroup
	- must not be the real name (i.e. "WW" instead of "World Wide")
4. Call Type
	- `Group Call` or `Private Call`
5. Call Alert
	- `None`
6. Create Channel
	- `TRUE` for the most common talk groups
7. Add To List
	- `TRUE` if the talkgroup should be accessible via the menu on the HT
8. Network
	- Is it a Brandmeister (`BM`) or IPSC2 (`I2`) talk group?
9. TimeSlot
	- on which timeslot should the talkgroup be sent? `1` or `2`

## Defaults Files

**Caution:** If there are talkgroups mentioned in `/defaults/Channel.csv`, then those talkgroups must be present in `/input/talkgroups.csv`.

Default files are merged with the created files to add custom channels and zones (i.e. channels `ISS Voice` and `ISS APRS` in Zone `ISS`).

Custom Zones that contain generated channels must be created in CPS after import.

## Remarks

1. bandwidth is always set to 12.5K as that is the predominant analog FM raster in Europe. Also, DMR requires 12.5;

## Known Issues

1. Some Repeater have different settings on repeater.oevsv.at and dmraustria.at
2. Scan List improvement: with promiscuous mode on, the digital scan lists could be improved