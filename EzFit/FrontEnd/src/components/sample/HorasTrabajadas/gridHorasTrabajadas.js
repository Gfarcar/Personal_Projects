import React from 'react';
import {
  DataGrid,
  Editing,
  Column
} from "devextreme-react/data-grid";
import "devextreme/dist/css/dx.light.css";
import DashboardCard from 'src/components/shared/DashboardCard';
import { useHorasTrabajadas } from './horasTrabajadasContext';

const GridHorasTrabajadas = () => {
  const { store } = useHorasTrabajadas();

  return (
    <DashboardCard title="Tabla Alimentos">
      <DataGrid
        dataSource={store}
        showBorders={true}
        repaintChangesOnly={true}
        height={600}
      >
        <Editing
          mode="row"
          allowAdding={true}
          allowDeleting={true}
          allowUpdating={true}
        />
        <Column dataField="food"/>
        <Column dataField="caloricValue" format={{ type: "fixedPoint", precision: 2 }} />
        <Column dataField="fat" format={{ type: "fixedPoint", precision: 2 }} />
        <Column dataField="carbohydrates" format={{ type: "fixedPoint", precision: 2 }} />
        <Column dataField="sugars" format={{ type: "fixedPoint", precision: 2 }} />
        <Column dataField="protein" format={{ type: "fixedPoint", precision: 2 }} />
      </DataGrid>
    </DashboardCard>
  );
}

export default GridHorasTrabajadas;