# HackHarvard

## Project Overview
This source code supplements various hardware components of a prototype, home-installed system which allows users to pay for their electricity usage using a stream of microtransactions, made possible mainly by the Interledger API.

## Motivation for the Project
The main real-world issue that this challenge sought to solve, was that of disruptive power-cuts and load-shedding events in developing countries. In a typical city or region troubled with disruptive power-cuts, the amount of energy generated (in kWh) cannot meet consumer energy demands. When the gap between supply and demand starts to grow, power-cuts and load-shedding events are instituted to control energy usage, a process which is quite disruptive to residents and organizations.

One of the contributing factors to this situation is the fact that the consumers are essentially unaware of the consequences of energy wastage and the correlation between energy wastage and future power-cuts/load-shedding events.

The system we designed switched consumers from a monthly charge to a real-time charge. Consumers would be paying in real-time for their energy usage via microtransactions.

The benefit of this is that power-generation/distribution entities can change the cost of each micro-packet of energy (measured in micro-Watt-seconds) in real-time, depending on the available energy in a given grid region. This has the effect of naturally incentivizing people to use less energy during a shortage, and also to use less energy overall.

It was our hope and belief that such a system could be used to provide a better solution to the currently implemented method, allowing people to live better lives, and to conserve energy as a whole in an attempt to make a positive impact on the environment.

## System Overview
### Arduino Uno as a Sensor
A small motor was connected to analog input pins on an Arduino Uno. This small motor simulated a load (such as a house in a power grid), where the arduino measured the current and voltage output to the motor, and processed this data into power consumption characteristics, ready to be output over the serial port.

### Raspberry Pi as Grid Controller
A raspberry pi was used to serve as a controller for a house with one or more sensors. The raspberry pi periodically queried the arduino-based sensor for power consumption information (transmitted over the serial port), and used this information to send API requests to the sample grid server.

### Grid Server
Our grid server received information from the raspberry pi and used this to send payments using the Interledger protocol.

## Project Outcome
The innovative use of the interledger protocol, complete with a working model of changing payment packet sizes based on the power consumption of the motor, lead our team to win the First Prize Award of US$10,000 for the Ripple Interledger Challenge at HackHarvard18.
