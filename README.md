# ATR2C

The ATR2C - AnyTone Repeater 2 CSV - parses the ÖVSV repeater list (`.xlsx` converted to `.csv`) for **70cm** and **2m** voice repeaters. It then creates zones and channels based on the data. It also adds default data if specified and an AnalogAddressBook containing the EchoLink DTMF codes for the respective repeater. 

The program
1. creates a *zone* for each DMR repeater containing
   1. n channels with the Tx and Rx frequencies of the channel with a predefined TG (n = number of talkgroups defined for the network of the repeater, defined in `/input/talkgroups.csv`)
2. creates a *zone* for every region (OE1 - OE9) containing all analog repeaters of the region.
3. creates an Analog Address Book containing the EchoLink DTMF Codes for every repeater in the input file
4. creates a digital and analog Scanlist for every location prefix (i.e. "OE3", "HB9") containing all analog repeaters and selected talkgroups of a the region's digital repeaters
5. merges predefined zones, channels, talkgroups and scanlists with the generated lists

## Usage and troubleshooting

Please refer to the [wiki](https://github.com/nitricware/ATR2C/wiki) for more information on how to use the program.

## Remarks

1. bandwidth is always set to 12.5K as that is the predominant analog FM raster in Europe. Also, DMR requires 12.5;
2. Custom CTCSS is set to fixed values; It's ignored by CPS;
3. Scan List improvement: with promiscuous mode on, the digital scan lists could be improved