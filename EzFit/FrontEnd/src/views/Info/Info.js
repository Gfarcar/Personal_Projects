import React from 'react';
import { useFormik } from 'formik';
import { Box, Grid } from '@mui/material';
import GridEmpleados from '../../components/sample/HorasTrabajadas/gridHorasTrabajadas';
import InfoForm  from '../../components/forms/theme-elements/InfoForm';
import InfoDisplay  from '../../components/sample/InfoDisplay/InfoDisplay';
const BCrumb = [
  {
    to: '/',
    title: 'Home',
  },
  {
    title: 'Productos',
  },
];

const Info = () => {
  return (
   
    
                    <Box>
                      <Grid container spacing={3}>
                        <Grid item sm={10} lg={6}>
                          <InfoDisplay />
                        </Grid>
                        {/* column */}
                        <Grid item xs={10} lg={6}>
                          <InfoForm />
                        </Grid>


                        
                      </Grid>
                    </Box>
  );
};

export default Info;
