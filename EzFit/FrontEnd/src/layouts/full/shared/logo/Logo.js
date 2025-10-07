import { useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { ReactComponent as LogoDark } from 'src/assets/images/logos/EzFit.svg';
import { ReactComponent as LogoDarkRTL } from 'src/assets/images/logos/EzFit.svg';
import { ReactComponent as LogoLight } from 'src/assets/images/logos/EzFit.svg';
import { ReactComponent as LogoLightRTL } from 'src/assets/images/logos/EzFit.svg';
import { styled, useTheme } from '@mui/material';

const Logo = () => {
  const theme = useTheme();
  const customizer = useSelector((state) => state.customizer);
  const LinkStyled = styled(Link)(() => ({
    height: customizer.TopbarHeight,
    width: customizer.isCollapse ? '40px' : '70px',
    overflow: 'hidden',
    display: 'block',
    fill: theme.palette.primary.main,
  }));

  if (customizer.activeDir === 'ltr') {
    return (
      <LinkStyled to="/" style={{
        display: 'flex',
        alignItems: 'center',
      }}>
        {customizer.activeMode === 'dark' ? (
          <LogoLight />
        ) : (
          <LogoDark />
        )}
      </LinkStyled>
    );
  }
  return (
    <LinkStyled to="/" style={{
      display: 'flex',
      alignItems: 'center',
    }}>
      {customizer.activeMode === 'dark' ? (
        <LogoDarkRTL />
      ) : (
        <LogoLightRTL />
      )}
    </LinkStyled>
  );
};

export default Logo;
