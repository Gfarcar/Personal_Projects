import React from 'react';
import {
  DataGrid,
  Editing
} from "devextreme-react/data-grid";
import "devextreme/dist/css/dx.light.css";
import DashboardCard from 'src/components/shared/DashboardCard';
import { useMenuItems } from './menuItemsContext'; // Asegúrate de que la ruta sea correcta

const GridMenuItems = () => {
  const { store } = useMenuItems();

  return (
    <DashboardCard title="Menu Items">
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

export default GridMenuItems;
