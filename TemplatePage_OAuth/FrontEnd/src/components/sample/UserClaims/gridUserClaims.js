import React from 'react';
import {
  DataGrid,
  Editing,
  Column
} from "devextreme-react/data-grid";
import "devextreme/dist/css/dx.light.css";
import DashboardCard from 'src/components/shared/DashboardCard';
import { useUserClaims } from './userClaimsContext'; // Asegúrate de que la ruta sea correcta

const GridUserClaims = () => {
  const { store } = useUserClaims();

  return (
    <DashboardCard title="Tabla de User Claims">
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

export default GridUserClaims;
