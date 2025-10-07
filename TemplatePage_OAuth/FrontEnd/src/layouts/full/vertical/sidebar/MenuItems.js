import {
  IconAward,
  IconBoxMultiple,
  IconPoint,
  IconBan,
  IconStar,
  IconMoodSmile,
  IconAperture
} from '@tabler/icons';

import { uniqueId } from 'lodash';

const Menuitems = [
  {
    navlabel: true,
    subheader: 'Dashboard',
  },

  {
    id: 15116,
    title: 'Bienvenidos',
    icon: IconAperture,
    href: '/Welcome',
    chip: 'New',
    chipColor: 'secondary',
  }
];

export default Menuitems;
