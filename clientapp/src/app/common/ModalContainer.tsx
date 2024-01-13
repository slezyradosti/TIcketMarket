import { Modal } from "semantic-ui-react";
import { useStore } from "../stores/store";
import { observer } from "mobx-react";

function ModalContainer() {
    const { modalStore } = useStore();

    return (
        <Modal
            open={modalStore.modal.open}
            onClose={modalStore.closeModal}
            size='mini'
        >
            <Modal.Content style={{ backgroundColor: '#eaeaea' }}>
                {modalStore.modal.body}
            </Modal.Content>
        </Modal>
    );
}

export default observer(ModalContainer);