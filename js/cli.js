#!/usr/bin/env node
'use strict';
const meow = require('meow');
const foodApp = require('.');

const cli = meow(`
	Usage
	  $ food-app [input]

	Options
	  --foo  Lorem ipsum [Default: false]

	Examples
	  $ food-app
	  unicorns & rainbows
	  $ food-app ponies
	  ponies & rainbows
`);

console.log(foodApp(cli.input[0] || 'unicorns'));
