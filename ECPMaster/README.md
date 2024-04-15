# Setup RL server

```bash
ssh root@ip
# create user and enable ssh
adduser ecp
passwd ecp
usermod -aG wheel ecp
ssh-keygen
mkdir -p ~/.ssh
cat ~/.ssh/id_rsa.pub >> ~/.ssh/authorized_keys
```

# Add a ansible config file for store credentials
- create a inventory file myHosts.txt
```
16.170.140.133 ansible_ssh_user=root ansible_ssh_pass=sandevdsic123
```
- mkdir `/etc/ansible`
- nano `/etc/ansible/ansible.cfg`
```[defaults]
inventory = /home/ubuntu/myHosts.txt
```
- test connection by
`ansible all -m package -a "name=net-tools state=present"`
