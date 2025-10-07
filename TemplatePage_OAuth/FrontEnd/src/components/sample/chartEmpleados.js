import React from 'react';
import {
  Chart,
  Series,
  ArgumentAxis,
  ValueAxis,
  Tooltip
} from 'devextreme-react/chart';
import "devextreme/dist/css/dx.light.css";
import { useStore } from './storeContext';
import DashboardCard from 'src/components/shared/DashboardCard';

const ChartEmpleados = () => {
  const { store, refreshKey } = useStore();

  return (
    <DashboardCard title="Empleados Sueldo Chart">
      <Chart
        dataSource={store}
        title="Sueldo por Empleado"
        key={refreshKey}
        palette={["#da0e0e", "#ff6666", "#a10000", "#f4c542", "#808080", "#5a2e2e", "#ffffff"]}
      >
        <ArgumentAxis>
          <ArgumentAxis label={{ rotationAngle: 45 }} />
        </ArgumentAxis>
        <ValueAxis>
          <ValueAxis title="Sueldo" />
        </ValueAxis>
        <Series
          argumentField="nombre"
          valueField="sueldo"
          type="bar"
          
        />
        <Tooltip enabled={true} />
      </Chart>
    </DashboardCard>
  );
}

export default ChartEmpleados;

