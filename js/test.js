import test from 'ava';
import foodApp from '.';

test('title', t => {
	t.throws(() => {
		foodApp(123);
	}, {
		instanceOf: TypeError,
		message: 'Expected a string, got number'
	});

	t.is(foodApp('unicorns'), 'unicorns & rainbows');
});
