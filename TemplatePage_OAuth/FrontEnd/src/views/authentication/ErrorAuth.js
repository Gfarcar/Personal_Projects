import { Box, Container, Typography, Button } from '@mui/material';
import { Link } from 'react-router-dom';
import ErrorImg from 'src/assets/images/backgrounds/errorimg.svg';
const ErrorAuth = () => (
  <Box
    display="flex"
    flexDirection="column"
    height="100vh"
    textAlign="center"
    justifyContent="center"
  >
    <Container maxWidth="md">
      <img src={ErrorImg} alt="404" />
      <Typography align="center" variant="h1" mb={4}>
        Opps!!!
      </Typography>
      <Typography align="center" variant="h4" mb={4}>
        You don't have permissions to view the page you are looking for
      </Typography>
      <Button
        color="primary"
        variant="contained"
        component={Link}
        to="/Welcome"
        disableElevation
      >
        Go Back to Home
      </Button>
    </Container>
  </Box>
);

export default ErrorAuth;
