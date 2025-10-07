import React from 'react';
import {
  DataGrid,
  Editing,
  Column
} from "devextreme-react/data-grid";
import "devextreme/dist/css/dx.light.css";
import DashboardCard from 'src/components/shared/DashboardCard';
import { useRoleTable } from './roleTableContext'; // Asegúrate de que la ruta sea correcta

const GridRoleTable = () => {
  const { store } = useRoleTable();

  return (
    <DashboardCard title="Tabla de Roles">
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
  
      </DataGrid>
    </DashboardCard>
  );
}

export default GridRoleTable;
