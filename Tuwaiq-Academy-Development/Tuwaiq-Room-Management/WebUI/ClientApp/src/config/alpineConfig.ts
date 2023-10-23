import Alpine from 'alpinejs';
import TimeAgo from '@marcreichel/alpine-timeago';
import AutoAnimate from '@marcreichel/alpine-auto-animate';
import Typewriter from '@marcreichel/alpine-typewriter';
import Tooltip from '@ryangjchandler/alpine-tooltip';
import Toolkit from '@alpine-collective/toolkit';
import morph from '@alpinejs/morph';
import collapse from '@alpinejs/collapse';

Alpine.plugin(Tooltip);
Alpine.plugin(Typewriter);
Alpine.plugin(TimeAgo);
Alpine.plugin(Toolkit);
Alpine.plugin(AutoAnimate);
Alpine.plugin(morph);
Alpine.plugin(collapse);



window.Alpine = Alpine;

export default Alpine;
