import React, { useEffect, useState } from 'react';
import { Chart, ArgumentAxis, ValueAxis, Series, Title } from 'devextreme-react/chart';
import DashboardCard from 'src/components/shared/DashboardCard';
import { useSalarios } from './salariosContext'; // Import context

const ChartSalarios = () => {
    const { store, refreshKey } = useSalarios(); // Access the store and refreshKey
    const [chartData, setChartData] = useState([]);

    useEffect(() => {
        // Load chart data when the component mounts or when refreshKey changes
        store.load().then((data) => {
            // Assuming data comes in an array and has the required fields
            const formattedData = data.map((item) => ({
                names: item.names,      // X-axis value
                monto: item.monto      // Y-axis value (salary)
            }));
            setChartData(formattedData);
        }).catch(error => {
            console.error('Error loading chart data:', error);
        });
    }, [store, refreshKey]);

    return (
      <DashboardCard title="Tabla Salarios">
          <Chart 
          dataSource={chartData}
          palette={["#da0e0e", "#ff6666", "#a10000", "#f4c542", "#808080", "#5a2e2e", "#ffffff"]}
          >
            <ArgumentAxis>
                  <Title text="Names" />
              </ArgumentAxis>
              <ValueAxis>
                  <Title text="Monto (Salary)" />
              </ValueAxis>
                <Series
                    valueField="monto"   // Ensure 'monto' matches the mapped data field
                    argumentField="names" // Ensure 'name' matches the mapped data field
                    type="bar"
                    
                />
          </Chart>

      </DashboardCard>
    );
};

export default ChartSalarios;
