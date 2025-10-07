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
    subheader: 'Home',
  },

  {
    id: 15118,
    title: 'HomePage',
    icon: IconAperture,
    href: '/HomePage',
    chipColor: 'secondary',
  },
  {
    id: 15116,
    title: 'Productos',
    icon: IconAperture,
    href: '/Alimentos',
    chipColor: 'secondary',
  },
  {
    id: 15117,
    title: 'Info',
    icon: IconAperture,
    href: '/Info',
    chipColor: 'secondary',
  },
  
];

export default Menuitems;
